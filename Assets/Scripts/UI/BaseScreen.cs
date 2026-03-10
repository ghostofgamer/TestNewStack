using UnityEngine;
using Zenject;

public abstract class BaseScreen : MonoBehaviour
{
    protected ServiceUI ServiceUI;

    [Inject]
    public void Construct(ServiceUI serviceUI)
    {
        ServiceUI = serviceUI;
        Debug.Log("Construct BaseScreen");
    }

    public virtual void Initialize()
    {
    }

    public virtual void Close()
    {
        ServiceUI.CloseWindow(this);
    }
}