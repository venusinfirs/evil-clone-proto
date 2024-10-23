using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    
    public class Player : Actor
    {
        [Inject] private InputHandler _inputHandler;
        [Inject] private SpawnPoint _spawnPoint;
        [Inject] private ReproduceActionService _reproService;

        private Vector2 _currentPlayerPosition; 
      
        protected override void Start()
        {
            base.Start();
            
            _inputHandler.OnSpacePressed += Jump;
            _inputHandler.HorizontalInput += Move;
            _inputHandler.OnRPressed += MoveToSpawnPoint;
        }
        
        private void OnDestroy()
        {
            _inputHandler.OnSpacePressed -= Jump;
            _inputHandler.HorizontalInput -= Move;
            _inputHandler.OnRPressed -= MoveToSpawnPoint;
        }
        
        private void MoveToSpawnPoint()
        {
            transform.position = _spawnPoint.transform.position; 
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
    }
}