using DefaultNamespace;
using UnityEngine;
using Zenject;

public class Actor : MonoBehaviour
{
    protected Animator CollisionAnim;
    
    private Transform _groundCheck;  
    private Rigidbody2D _rBody;
    private bool _isGrounded;
    
    [Inject] private ReproduceActionService _reproService;
    
    protected virtual void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _groundCheck = gameObject.transform.Find(GameplayValues.GroundCheckTag);
        CollisionAnim = gameObject.transform.GetComponentInChildren<Animator>();
        _rBody.constraints = RigidbodyConstraints2D.FreezeRotation; 
    }

    protected virtual void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, GameplayValues.GroundCheckRadius, 64); 
        if (_isGrounded)
        {
            _rBody.velocity = new Vector2(_rBody.velocity.x, GameplayValues.JumpForce);
        }
    }

    protected virtual void Move(float moveInput)
    {
        _rBody.velocity = new Vector2(moveInput * GameplayValues.MoveSpeed, _rBody.velocity.y);
    }
    
}
