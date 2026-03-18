using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
        [SerializeField] private PersistentLoadingScreen _loadingScreenPrefab;
        
    public override void InstallBindings()
    {
        Container.BindInterfacesTo<AudioService>().AsSingle();
        Container.Bind<PersistentLoadingScreen>().FromComponentInNewPrefab(_loadingScreenPrefab).AsSingle()
            .NonLazy(); // создаём сразу при старте игры
    }
}