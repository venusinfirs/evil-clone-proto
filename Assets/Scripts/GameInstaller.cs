using DefaultNamespace;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject clonePrefab; 
    
    public override void InstallBindings()
    {
        Container.Bind<GameObject>().FromInstance(clonePrefab).AsTransient();
        
        Container.Bind<InputHandler>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<SpawnPoint>().FromComponentInHierarchy().AsSingle();
        Container.Bind<CloneFactory>().AsTransient();
        Container.Bind<ReproduceActionService>().AsSingle(); 
    }
}