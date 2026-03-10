using UnityEngine;
using Zenject;

public class AddresableWindow : MonoBehaviour
{

    [Inject]
    public void Construct()
    {
        Debug.Log("Construct");
    }

    public void Initialize()
    {
        Debug.Log("SettingsWindow Initialized");
    }

    public void Close()
    {
        AddresableWindowManager.Instance.CloseWindow(this);
    }
}
