using UnityEngine;

public class PlayerController : MonoBehaviour
{
   [SerializeField] private float moveSpeed = 5f;       
   [SerializeField] private float jumpForce = 10f;      
   [SerializeField] private Transform groundCheck;       
   [SerializeField] private float groundCheckRadius = 0.2f;  
   [SerializeField] private LayerMask groundLayer;      

    private Rigidbody2D _rBody;
    private bool _isGrounded;

    private const string HorizontalAxisName = "Horizontal";
    private const string JumpKeyName = "Jump";

    void Start()
    {
        _rBody = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        float moveInput = Input.GetAxis(HorizontalAxisName);
        _rBody.velocity = new Vector2(moveInput * moveSpeed, _rBody.velocity.y);
        
        if (_isGrounded && Input.GetButtonDown(JumpKeyName))
        {
            _rBody.velocity = new Vector2(_rBody.velocity.x, jumpForce);
        }
    }
}
