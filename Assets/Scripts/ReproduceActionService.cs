using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace DefaultNamespace
{
    public class ReproduceActionService
    {
        private Queue<ActionInfo> _playerActions = new Queue<ActionInfo>(); 
        public Queue<ActionInfo> PlayerActions => _playerActions;

        public event Action OnJump;
        public event Action<float> OnMove;
        
        
        public void LogAction(ActionKind kind, float? axis)
        {
           // _playerActions.Enqueue(new ActionInfo(kind, Time.time, axis));
            //Debug.Log($"Log movement {kind}, {Time.time}, {axis}");
            
            DelayActionReproduce(kind, axis);

        }

        private async UniTaskVoid DelayActionReproduce(ActionKind kind, float? axis)
        {
            await UniTask.Delay((int)(GameplayValues.ActionDelay * 1000));
            
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