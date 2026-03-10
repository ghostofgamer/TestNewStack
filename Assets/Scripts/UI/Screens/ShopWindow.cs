using UI.Screens;
using UnityEngine;
using Zenject;

public class ShopWindow : BaseScreen
{
    [Inject]
    public void Construct(ServiceUI serviceUI)
    {
        base.Construct(serviceUI);
        Debug.Log("Construct ShopWindow");
    }
}