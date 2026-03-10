using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public class AddresableWindowManager : MonoBehaviour
{
    public static AddresableWindowManager Instance;

    [SerializeField] private Transform _windowRoot;
    [Inject] private DiContainer _container;
    
    private void Awake() => Instance = this;

    private Dictionary<string, AddresableWindow> _openWindows = new();

    public async void OpenWindow(string key)
    {
        if (_openWindows.ContainsKey(key))
            return; // окно уже открыто

        // Асинхронная загрузка префаба
        var handle = Addressables.InstantiateAsync(key, _windowRoot);
        await handle.Task;

        GameObject instance = handle.Result;
        _container.InjectGameObject(instance);
        
        var window = handle.Result.GetComponent<AddresableWindow>();
        window.Initialize();

        _openWindows.Add(key, window);
    }

    public void CloseWindow(AddresableWindow window)
    {
        if (window == null)
            return;

        string keyToRemove = null;
        foreach (var kvp in _openWindows)
        {
            if (kvp.Value == window)
            {
                keyToRemove = kvp.Key;
                break;
            }
        }

        if (keyToRemove != null)
        {
            _openWindows.Remove(keyToRemove);
            Addressables.ReleaseInstance(window.gameObject);
        }
    }
}
