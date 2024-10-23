using System;
using DefaultNamespace;
using UnityEngine;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
    [Inject] private InputHandler _inputHandler;
    [Inject] private CloneFactory _cloneFactory;

    private Vector2 _lastSpawnedPosition;
    private int _clonesCount;
    
    [Inject]
    private DiContainer _container; 
    void Awake()
    {
        _inputHandler.OnRPressed += SpawnClone;
    }

    private void SpawnClone()
    {
        var clone = _cloneFactory.CreateClone(transform.position);
        _container.InstantiateComponent<EvilClone>(clone);
        
        if (_clonesCount > 0)
        {
            clone.transform.position = new Vector2(_lastSpawnedPosition.x + GameplayValues.SpawnGap, transform.position.y);
        }

        _lastSpawnedPosition = clone.transform.position;
        _clonesCount++;
    }

    private void OnDestroy()
    {
        _inputHandler.OnRPressed -= SpawnClone;
    }
}
