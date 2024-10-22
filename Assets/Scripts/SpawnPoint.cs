using DefaultNamespace;
using UnityEngine;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
    [Inject] private InputHandler _inputHandler;
    [Inject] private CloneFactory _cloneFactory;
    void Awake()
    {
        _inputHandler.OnRPressed += SpawnClone;
    }

    private void SpawnClone()
    {
        var clone = _cloneFactory.CreateClone(transform.position);
    }
}
