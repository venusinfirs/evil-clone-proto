using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class EvilClone : Actor
    {
        [Inject] private ReproduceActionService _reproService;
        private static readonly int Collision1 = Animator.StringToHash("Collision");
        private int _currentInstance; 

        protected override void Start()
        {
            base.Start();

            _reproService.OnJump += Jump;
            _reproService.OnMove += Move;
            _reproService.CycleEnded += OnCycleEnded;
            
            Perform().Forget();
        }
        private void OnDestroy()
        {
            _reproService.OnJump -= Jump;
            _reproService.OnMove -= Move;
            _reproService.CycleEnded -= OnCycleEnded;
        }
        
        private void OnCycleEnded(int number)
        {
            if (_currentInstance == number)
            {
                _reproService.OnJump -= Jump;
                _reproService.OnMove -= Move;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(GameplayValues.PlayerTag))
            {
                CollisionAnim.SetTrigger(Collision1);
            }
        }
        
        private async UniTaskVoid Perform()
        {
            await UniTask.Delay(500); 
            _reproService.ReproduceActions();
            _currentInstance = _reproService.CyclesCount;
            _reproService.IncrementRespawnsCount();
        }
    }
}