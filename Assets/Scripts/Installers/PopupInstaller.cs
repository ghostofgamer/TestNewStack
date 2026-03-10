using UI.Popup;
using UnityEngine;
using Zenject;

public class PopupInstaller : MonoInstaller
{
    [SerializeField] private Transform _popupRoot;
    [SerializeField] private PopupConfigs _popupConfig;

    public override void InstallBindings()
    {
        // Bind PopupService
        Container.Bind<PopupService>().AsSingle().NonLazy();

        // Bind root и config для конструктора
        Container.Bind<Transform>().FromInstance(_popupRoot).AsSingle();
        Container.Bind<PopupConfigs>().FromInstance(_popupConfig).AsSingle();
    }
}