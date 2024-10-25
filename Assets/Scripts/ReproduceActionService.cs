using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class ReproduceActionService
    {
        private Dictionary<int, CloneActions> _cloneActions = new Dictionary<int, CloneActions>();
        private int _cyclesCount;
        
        public void IncrementRespawnsCount()
        {
            _cyclesCount++;
        }

        public void LogAction(ActionKind kind, float startTime, float? axis)
        {
            if (!_cloneActions.TryGetValue(_cyclesCount, out CloneActions actions))
            {
                actions = new CloneActions();
                _cloneActions.Add(_cyclesCount, actions);
            }
            
            actions.SetAction(new ActionInfo(kind, startTime, Time.time, _cyclesCount, axis));
            
           // UnityEngine.Debug.Log($"[ReproService] cycle {CyclesCount}, kind: {kind}, startTime {startTime}, endTime {Time.time}, axis {axis}");
        }

        public void ReproduceActions(EvilClone cloneInstance)
        {
            ProcessActionsQueue(_cyclesCount, cloneInstance).Forget();
        }

        private async UniTaskVoid ProcessActionsQueue(int cycleNum, EvilClone cloneInstance)
        {
            UnityEngine.Debug.Log($"[ReproService] ProcessActionsQueue {cycleNum}");
            if (!_cloneActions.TryGetValue(cycleNum, out var cloneActions))
            {
                return;
            }
            
            cloneActions.SetCloneInstance(cloneInstance);

            float? previousActionEndTime = null; 
            while (cloneActions.ActionCycles.Count > 0)
            {
                try
                {
                    var act = cloneActions.ActionCycles.Dequeue();
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

            _cloneActions.Remove(cycleNum);
        }
        private async UniTask<float> SimulateInput(ActionInfo info)
        {
            var elapsedTime = 0f;
            var duration = info.EndTime - info.StartTime;
            var previousActionEndTime = info.EndTime;

            if (duration <= 0)
            {
                Perform(info);
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
            Perform(info);
            await UniTask.WaitForEndOfFrame();
        }

        private async UniTask DelayBetweenActions(float delay)
        {
            var res = (int)(delay * GameplayValues.ActionDelay);
            await UniTask.Delay(res > 0 ? res : 0);
        }
        
        private void Perform(ActionInfo info)
        {
            var isInstance = _cloneActions.TryGetValue(info.Index, out var clone);
           
            if (!isInstance)
            {
                return;
            }

            switch (info.Kind)
            {
                case ActionKind.Jump:
                    clone.CloneInstance.Jump();
                    break;
                case ActionKind.Move:
                    clone.CloneInstance.Move(info.Axis ?? 0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}