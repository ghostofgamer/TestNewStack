using System.Collections.Generic;
using UI.Popup;
using UnityEngine;

[CreateAssetMenu(fileName = "UIPopupsConfig", menuName = "Configs/UI Popups Config")]
public class PopupConfigs : ScriptableObject
{
    [field: SerializeField] public List<BasePopup<PopupData>> Popups { get; private set; }
}
