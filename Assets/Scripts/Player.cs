using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    
    public class Player : MonoBehaviour
    {
        [Inject] private InputHandler _inputHandler;
        [Inject] private SpawnPoint _spawnPoint;
        [Inject] private ReproduceActionService _reproService;

        private Vector2 _currentPlayerPosition; 
        
        private Transform groundCheck;  
        private Rigidbody2D _rBody;
        private bool _isGrounded;
        private void Start()
        {
            _rBody = GetComponent<Rigidbody2D>();
            groundCheck = gameObject.transform.Find("GroundCheck");
            _rBody.constraints = RigidbodyConstraints2D.FreezeRotation; 
            
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

        private void Jump()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, GameplayValues.GroundCheckRadius, 64); 
            if (_isGrounded)
            {
                _rBody.velocity = new Vector2(_rBody.velocity.x, GameplayValues.JumpForce);
            }
            _reproService.LogAction(ActionKind.Jump, null);
        }

        private void Move(float moveInput)
        {
            _rBody.velocity = new Vector2(moveInput * GameplayValues.MoveSpeed, _rBody.velocity.y);
            _reproService.LogAction(ActionKind.Move, moveInput);
        }

        private void MoveToSpawnPoint()
        {
            transform.position = _spawnPoint.transform.position; 
        }
    }
}