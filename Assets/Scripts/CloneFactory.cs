using UnityEngine;

namespace DefaultNamespace
{
    public class CloneFactory
    {
        private readonly EvilClone _enemyPrefab; 

        public CloneFactory (EvilClone clonePrefab)
        {
            _enemyPrefab = clonePrefab;
        }

        public EvilClone CreateClone(Vector3 position)
        {
            return Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
        }
    }
}