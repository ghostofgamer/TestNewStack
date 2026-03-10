using Enums;
using UnityEngine;
using Zenject;

public class UIWindowButton : AbstractButton
{
    /*[SerializeField] private UIWindowType windowType;

    private UIService _uiService;

    [Inject]
    public void Construct(UIService uiService)
    {
        _uiService = uiService;
        Debug.Log("UIService Инжектится!");
    }
    
    public override void OnClick()
    {
        if (_uiService == null)
        {
            Debug.LogWarning("UIService не инжектирован!");
            return;
        }
        
        Debug.LogWarning("UIService инжектирован!");
        _uiService.ToggleWindow(windowType);
    }*/
    
    [SerializeField]private WindowsType _windowsType;
    
    private ServiceUI _serviceUI;
    
    [Inject]
    public void Construct(ServiceUI uiService)
    {
        _serviceUI = uiService;
        Debug.Log("UIService Инжектится!");
    }
    
    public override void OnClick()
    {
        if (_serviceUI == null)
        {
            Debug.LogWarning("_serviceUI не инжектирован!");
            return;
        }
        
        Debug.LogWarning("UIService инжектирован!");
        _serviceUI.OpenWindow(_windowsType);
    }
}