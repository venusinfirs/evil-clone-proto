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
        private Renderer _renderer;

        protected override void Start()
        {
            base.Start();
            _renderer = GetComponent<Renderer>();
            
            Perform().Forget();
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
            _reproService.ReproduceActions(this);
            _reproService.IncrementRespawnsCount();
        }

        public void Stupify()
        {
            _renderer.material.color = Color.gray;
        }
    }
}