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
        private float? _arrowInputStartTime;

        protected override void Start()
        {
            base.Start();
            
            InputHandler.OnSpacePressed += Jump;
            InputHandler.HorizontalInput += Move;
            InputHandler.OnHorizontalKeyUp += OnEndInput;
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
            DelayRespawn().Forget();
        }
        private async UniTaskVoid DelayRespawn()
        {
            await UniTask.Delay(GameplayValues.PlayerRespawnDelay); 
            var pos = _spawnPoint.transform.position;
            transform.position = new Vector2(pos.x + GameplayValues.PlayerSpawnShift, pos.y + GameplayValues.PlayerSpawnShift); 
        }

        public override void Jump()
        {
            base.Jump(); 
            
            _reproService.LogAction(ActionKind.Jump, Time.time, null);
        }

        public override void Move(float moveInput)
        {
            base.Move(moveInput);
            if (_arrowInputStartTime == null)
            {
                _arrowInputStartTime = Time.time;
            }
        }

        private void OnEndInput(float moveInput)
        {
            _reproService.LogAction(ActionKind.Move, _arrowInputStartTime ?? 0, moveInput);
            _arrowInputStartTime = null;
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