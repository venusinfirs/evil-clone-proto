using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;       
        [SerializeField] private float jumpForce = 10f;      
        [SerializeField] private Transform groundCheck;       
        [SerializeField] private float groundCheckRadius = 0.2f;  
        [SerializeField] private LayerMask groundLayer;
        
        [Inject] private InputHandler _inputHandler;
        [Inject] private SpawnPoint _spawnPoint;
        
        private Rigidbody2D _rBody;
        private bool _isGrounded;
        private void Start()
        {
            _rBody = GetComponent<Rigidbody2D>();
            
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
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if (_isGrounded)
            {
                _rBody.velocity = new Vector2(_rBody.velocity.x, jumpForce);
            }
        }

        private void Move(float moveInput)
        {
            _rBody.velocity = new Vector2(moveInput * moveSpeed, _rBody.velocity.y);
        }

        private void MoveToSpawnPoint()
        {
            transform.position = _spawnPoint.transform.position; 
        }
    }
}