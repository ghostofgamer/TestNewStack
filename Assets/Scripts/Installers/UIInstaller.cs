using System.Collections.Generic;
using Attention;
using UI;
using UI.Screens;
using UnityEngine;
using Zenject;

public class UIInstaller : MonoInstaller
{
    [Header("UI Configs")]
    [SerializeField] UIWindowsConfig windowsConfig;
    [SerializeField] Transform uiRoot;
    
    [Header("Popups")]
    [SerializeField] private AttentionHintPopup hintPopup;
    
    public override void InstallBindings()
    {
        Container.Bind<UIRoot>().FromComponentInHierarchy().AsSingle();
        Container.Bind<AttentionHintPopup>().FromInstance(hintPopup).AsSingle();
        Container.Bind<ServiceUI>().AsSingle();
        // Container.Bind<LevelUpPopup>().FromInstance(levelUpPopup).AsSingle();
        
        var windowInstances = new List<BaseWindow>();
        
        foreach (var prefab in windowsConfig.Windows)
        {
            var instance = Container.InstantiatePrefabForComponent<BaseWindow>(prefab, uiRoot);
            windowInstances.Add(instance);
        }
        
        Container.Bind<List<BaseWindow>>().FromInstance(windowInstances).AsSingle();
        Container.Bind<UIService>().AsSingle().NonLazy();
    }
}