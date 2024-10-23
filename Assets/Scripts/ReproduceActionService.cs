using System;
using Cysharp.Threading.Tasks;

namespace DefaultNamespace
{
    public class ReproduceActionService
    {
        public event Action OnJump;
        public event Action<float> OnMove;
        
        public void LogAction(ActionKind kind, float? axis)
        {
            DelayActionReproduce(kind, axis).Forget();
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