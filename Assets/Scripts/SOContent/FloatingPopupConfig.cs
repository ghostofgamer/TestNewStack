using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "FloatingPopupConfig", menuName = "Configs/FloatingPopupConfig")]
public class FloatingPopupConfig : ScriptableObject
{
    public FloatingPopupType type;
    public Sprite icon;
    public Color textColor;
    public bool showPlusSign = true; // для положительных чисел
}
