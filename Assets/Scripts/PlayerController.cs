using System;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 5f;       
   [SerializeField] private float jumpForce = 10f;      
   [SerializeField] private Transform groundCheck;       
   [SerializeField] private float groundCheckRadius = 0.2f;  
   [SerializeField] private LayerMask groundLayer;      

    private Rigidbody2D _rBody;
    private bool _isGrounded;

    [Inject] private InputHandler _inputHandler; 

    private void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _inputHandler.OnSpacePressed += Jump;
        _inputHandler.HorizontalInput += Move;
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

    private void OnDestroy()
    {
        _inputHandler.OnSpacePressed -= Jump;
        _inputHandler.HorizontalInput -= Move;
    }
}
