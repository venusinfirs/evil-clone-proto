using Cysharp.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class Actor : MonoBehaviour
{
    protected Animator CollisionAnim;
    
    private Transform _groundCheck;  
    private Rigidbody2D _rBody;
    private bool _isGrounded;
    private bool _speedUp; 
    
    [Inject] protected InputHandler InputHandler;
    
    protected virtual void Start()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _groundCheck = gameObject.transform.Find(GameplayValues.GroundCheckTag);
        CollisionAnim = gameObject.transform.GetComponentInChildren<Animator>();
        _rBody.constraints = RigidbodyConstraints2D.FreezeRotation;

        InputHandler.OnSpeedUpPressed += SpeedUp;
    }

    protected virtual void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, GameplayValues.GroundCheckRadius, GameplayValues.GroundLayerMask); 
        if (_isGrounded)
        {
            _rBody.linearVelocity = new Vector2(_rBody.linearVelocity.x, GameplayValues.JumpForce);
        }
    }

    protected virtual void Move(float moveInput)
    {
        var speed = GameplayValues.IsSpeedIncreased ? GameplayValues.IncreasedMoveSpeed : GameplayValues.MoveSpeed;
        _rBody.linearVelocity = new Vector2(moveInput * speed, _rBody.linearVelocity.y);
    }

    private void SpeedUp()
    {
        GameplayValues.SetSpeedIncreaseStatus(true);
        StartSpeedUp().Forget();
    }

    private async UniTaskVoid StartSpeedUp()
    {
        await UniTask.Delay((int)GameplayValues.SpeedUpTime); 
        GameplayValues.SetSpeedIncreaseStatus(false);
    }

    private void OnDestroy()
    {
        InputHandler.OnSpeedUpPressed -= SpeedUp;
    }
}
