using System.Collections.Generic;
using Attention;
using Enums;
using Interfaces;
using UI.Screens;
using UnityEngine;
using Zenject;

public class UIService
{
    private readonly AttentionHintPopup _hintPopup;
    private readonly Dictionary<UIWindowType, BaseWindow> _windows;
    
    [Inject]
    public UIService(List<BaseWindow> windows,AttentionHintPopup hintPopup)
    {
        _hintPopup = hintPopup;
        
        _windows = new Dictionary<UIWindowType, BaseWindow>();

        foreach (var window in windows)
        {
            if (window is IWindowTyped typed)
            {
                if (!_windows.ContainsKey(typed.Type))
                    _windows.Add(typed.Type, window);
                else
                    Debug.LogWarning($"UIService: окно с типом {typed.Type} уже добавлено!");
            }
        }
    }

    public void ShowHint(string message)
    {
        _hintPopup.Show(message);
    }

    public void ShowLevelUp(int level)
    {
        
    }
    
    public BaseWindow GetWindow(UIWindowType type)
    {
        _windows.TryGetValue(type, out var window);
        return window;
    }

    /// <summary>
    /// Проверка, открыто ли окно
    /// </summary>
    public bool IsOpen(UIWindowType type)
    {
        return _windows.TryGetValue(type, out var window) && window.gameObject.activeSelf;
    }
    
    public void ToggleWindow(UIWindowType type)
    {
        var window = GetWindow(type);
        if (window == null)
        {
            Debug.LogWarning($"UIService: окно с типом {type} не найдено!");
            return;
        }

        if (window.gameObject.activeSelf)
            window.Hide();
        else
            window.Show();
    }
}