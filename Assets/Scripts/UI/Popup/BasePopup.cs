using System;
using UI.Popup;
using UnityEngine;

public abstract class BasePopup<TData> : MonoBehaviour, IPopup where TData : PopupData
{
    public void Show(PopupData data, System.Action<IPopup> onComplete = null)
    {
        if (data is TData typedData)
        {
            ShowTyped(typedData, onComplete);
        }
        else
        {
            Debug.LogError($"Неверный тип данных для {GetType().Name}");
        }
    }

    protected abstract void ShowTyped(TData data, System.Action<IPopup> onComplete);
}