using System;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class EvilClone : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private float groundCheckRadius = 0.2f;
        
        private Transform groundCheck;
        private LayerMask groundLayer; 
        
        private bool _isGrounded;
        private Rigidbody2D _rBody;

        [Inject] private ReproduceActionService _reproService;

        private void Start()
        {
            _rBody = GetComponent<Rigidbody2D>();
            groundCheck = gameObject.transform.Find("GroundCheck");
            groundLayer = LayerMask.NameToLayer("Ground"); 
            Debug.DrawRay(groundCheck.position, Vector2.down * (groundCheckRadius + 0.1f), Color.red);
            _rBody.constraints = RigidbodyConstraints2D.FreezeRotation; 

            _reproService.OnJump += Jump;
            _reproService.OnMove += Move;

        }

       /* private void Update() // for debug
        {
            var actions = _reproService?.PlayerActions;
            if (actions is { Count: > 0 })
            {
                var action = actions.Dequeue();
                switch (action.Kind)
                {
                    case ActionKind.Jump:
                        Jump();
                        break;
                    case ActionKind.Move:
                        Move(action.Axis ?? 0f);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }*/

        private void Jump()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, 64);
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
            _reproService.OnJump -= Jump;
            _reproService.OnMove -= Move;
        }
    }
}