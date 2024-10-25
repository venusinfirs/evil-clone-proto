using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class ReproduceActionService
    {
        public event Action OnJump;
        public event Action<float> OnMove;
        public event Action<int> CycleEnded;
        public int CyclesCount { get; private set; }
        
        private Dictionary<int, Queue<ActionInfo>> _actionCycles = new Dictionary<int, Queue<ActionInfo>>();
        
        private float? _previousActionEndTime;
        public void IncrementRespawnsCount()
        {
            CyclesCount++;
        }

        public void LogAction(ActionKind kind, float startTime, float? axis)
        {
            if (!_actionCycles.TryGetValue(CyclesCount, out Queue<ActionInfo> actions))
            {
                actions = new Queue<ActionInfo>();
                _actionCycles.Add(CyclesCount, actions);
            }
            
            actions.Enqueue(new ActionInfo(kind, startTime, Time.time, axis));
            
            UnityEngine.Debug.Log($"[ReproService] cycle {CyclesCount}, kind: {kind}, startTime {startTime}, endTime {Time.time}, axis {axis}");
        }

        public void ReproduceActions()
        {
            ProcessActionsQueue(CyclesCount).Forget();
        }

        private async UniTaskVoid ProcessActionsQueue(int cycleNum)
        {
            if (!_actionCycles.TryGetValue(cycleNum, out var currentActions))
            {
                return;
            }

            float? previousActionEndTime = null; 
            while (currentActions.Count > 0)
            {
                try
                {
                    var act = currentActions.Dequeue();
                    if (previousActionEndTime != null)
                    {
                        await DelayBetweenActions(act.StartTime - previousActionEndTime.Value);
                    }

                    previousActionEndTime = await SimulateInput(act);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e.Message);
                }
            }

            if (currentActions.Count == 0)
            {
                CycleEnded?.Invoke(cycleNum);
                _actionCycles.Remove(cycleNum);
            }
        }
        private async UniTask<float> SimulateInput(ActionInfo info)
        {
            var elapsedTime = 0f;
            var duration = info.EndTime - info.StartTime;
            var previousActionEndTime = info.EndTime;

            if (duration <= 0)
            {
                Perform(info.Kind, info.Axis);
            }

            while (elapsedTime < duration)
            {
                await PerformEveryFrame(info);
                
                elapsedTime += Time.deltaTime;
            }
            
            await UniTask.WaitForSeconds(duration);
            return previousActionEndTime;
        }

        private async UniTask PerformEveryFrame(ActionInfo info)
        {
            Perform(info.Kind, info.Axis);
            await UniTask.WaitForEndOfFrame();
        }

        private async UniTask DelayBetweenActions(float delay)
        {
            var res = (int)(delay * GameplayValues.ActionDelay);
            await UniTask.Delay(res > 0 ? res : 0);
        }
        
        private void Perform(ActionKind kind, float? axis)
        {
            switch (kind)
            {
                case ActionKind.Jump:
                    OnJump?.Invoke();
                    break;
                case ActionKind.Move:
                    OnMove?.Invoke(axis ?? 0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}