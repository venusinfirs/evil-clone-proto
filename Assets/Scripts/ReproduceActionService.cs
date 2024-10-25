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

        public Queue<ActionInfo> _actions = new Queue<ActionInfo>(); 
        
        private float? _previousActionEndTime;
        
        public void LogAction(ActionKind kind, float startTime, float? axis)
        {
            _actions.Enqueue(new ActionInfo(kind, startTime, Time.time, axis));
            UnityEngine.Debug.Log($"[ReproService] kind: {kind}, startTime {startTime}, endTime {Time.time}, axis {axis}");
        }

        public void ReproduceActions()
        {
            ProcessActionsQueue().Forget();
        }

        private async UniTaskVoid ProcessActionsQueue()
        {
            while (_actions.Count > 0)
            {
                try
                {
                    var act = _actions.Dequeue();
                    if (_previousActionEndTime != null)
                    {
                        await DelayBetweenActions(act.StartTime - _previousActionEndTime.Value);
                    }

                    await SimulateInput(act);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogError(e.Message);
                }
            }
        }

        private async UniTask DelayBetweenActions(float delay)
        {
            await UniTask.Delay((int)delay * 1000);
        }

        private async UniTask SimulateInput(ActionInfo info)
        {
            var elapsedTime = 0f;
            var duration = info.EndTime - info.StartTime;
            _previousActionEndTime = info.EndTime;
            
            while (elapsedTime < duration)
            {
                Perform(info.Kind, info.Axis);
                
                await UniTask.WaitForEndOfFrame();
                elapsedTime += Time.deltaTime;
            }
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