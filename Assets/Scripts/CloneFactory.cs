using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CloneFactory
    {
        [Inject]
        private DiContainer _container;
       
        private readonly GameObject _clonePrefab; 

        public CloneFactory (GameObject clonePrefab)
        {
            _clonePrefab = clonePrefab;
        }

        public GameObject CreateClone(Vector3 position)
        {
            var instance = _container.InstantiatePrefab(_clonePrefab);
            instance.transform.position = position;
            return instance;
        }
    }
}