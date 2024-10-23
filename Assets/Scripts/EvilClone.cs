using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class EvilClone : Actor
    {
        [Inject] private ReproduceActionService _reproService;
        private static readonly int Collision1 = Animator.StringToHash("Collision");

        protected override void Start()
        {
            base.Start();

            _reproService.OnJump += Jump;
            _reproService.OnMove += Move;

        }

        private void OnDestroy()
        {
            _reproService.OnJump -= Jump;
            _reproService.OnMove -= Move;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayValues.PlayerTag))
            {
                CollisionAnim.SetTrigger(Collision1);
            }
        }
    }
}