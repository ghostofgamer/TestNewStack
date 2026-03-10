using UnityEngine;
using Zenject;

public class SettingsWindow : BaseScreen
{
    [Inject]
    public void Construct(ServiceUI serviceUI)
    {
        base.Construct(serviceUI);
        Debug.Log("Construct SettingsWindow");
    }
}
