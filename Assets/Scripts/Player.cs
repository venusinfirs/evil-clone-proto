using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    
    public class Player : Actor
    {
        [Inject] private SpawnPoint _spawnPoint;
        [Inject] private ReproduceActionService _reproService;

        private Vector2 _currentPlayerPosition;
        private static readonly int Collision1 = Animator.StringToHash("Collision");

        protected override void Start()
        {
            base.Start();
            
            InputHandler.OnSpacePressed += Jump;
            InputHandler.HorizontalInput += Move;
            InputHandler.OnRPressed += MoveToSpawnPoint;
        }
        
        private void OnDestroy()
        {
            InputHandler.OnSpacePressed -= Jump;
            InputHandler.HorizontalInput -= Move;
            InputHandler.OnRPressed -= MoveToSpawnPoint;
        }
        
        private void MoveToSpawnPoint()
        {
            StartSpeedUp().Forget();
        }
        private async UniTaskVoid StartSpeedUp()
        {
            await UniTask.Delay(2); 
            var pos = _spawnPoint.transform.position;
            transform.position = new Vector2(pos.x + GameplayValues.SpawnGap, pos.y); 
        }

        protected override void Jump()
        {
            base.Jump();
            _reproService.LogAction(ActionKind.Jump, null);
        }

        protected override void Move(float moveInput)
        {
            base.Move(moveInput);
            _reproService.LogAction(ActionKind.Move, moveInput);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayValues.EvilCloneTag))
            {
                CollisionAnim.SetTrigger(Collision1);
            }
        }
    }
}