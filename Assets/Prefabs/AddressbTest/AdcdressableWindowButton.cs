using UnityEngine;
using UnityEngine.UI;

public class AdcdressableWindowButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            AddresableWindowManager.Instance.OpenWindow("AddresableWindow");
        });
    }
}
