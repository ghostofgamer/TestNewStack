using System.Collections.Generic;
using UI.Screens;
using UnityEngine;

[CreateAssetMenu(fileName = "UIWindowsConfig", menuName = "Configs/UI Windows Config")]
public class UIWindowsConfig : ScriptableObject
{
  [field: SerializeField] public List<BaseWindow> Windows { get; private set; }
}
