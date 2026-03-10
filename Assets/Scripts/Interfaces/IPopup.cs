using UI.Popup;

public interface IPopup
{
    void Show(PopupData data, System.Action<IPopup> onComplete = null);
}