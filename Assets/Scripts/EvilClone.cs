using Zenject;

namespace DefaultNamespace
{
    public class EvilClone : Actor
    {
        [Inject] private ReproduceActionService _reproService;

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
    }
}