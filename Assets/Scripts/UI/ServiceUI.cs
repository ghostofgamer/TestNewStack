using System.Collections.Generic;
using Enums;
using UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class ServiceUI
{
    private UIRoot _uiRoot;
    private DiContainer _container;

    private Dictionary<WindowsType, BaseScreen> _openWindows = new();

    [Inject]
    private void Construct(DiContainer container, UIRoot uiRoot)
    {
        _container = container;
        _uiRoot = uiRoot;
    }

    public async void OpenWindow(WindowsType type)
    {
        if (_openWindows.ContainsKey(type))
            return;

        var handle = Addressables.InstantiateAsync(type.ToString(), _uiRoot.WindowRoot);
        await handle.Task;

        GameObject instance = handle.Result;

        // 🔥 Инжектим зависимости
        _container.InjectGameObject(instance);

        BaseScreen window = instance.GetComponent<BaseScreen>();

        window.Initialize();

        _openWindows.Add(type, window);
    }

    public void CloseWindow(BaseScreen window)
    {
        WindowsType keyToRemove = default;

        foreach (var kvp in _openWindows)
        {
            if (kvp.Value == window)
            {
                keyToRemove = kvp.Key;
                break;
            }
        }

        if (_openWindows.ContainsKey(keyToRemove))
            _openWindows.Remove(keyToRemove);

        Addressables.ReleaseInstance(window.gameObject);
    }
}