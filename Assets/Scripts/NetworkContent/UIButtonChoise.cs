using UnityEngine;
using UnityEngine.UI;

public class UIButtonChoise : MonoBehaviour
{
    [SerializeField]private int _choiseIndex;
    
    public PlayerRPS localPlayer;

     private Button _button;
     
    private void Awake()
    {
        // Получаем компонент Button на том же объекте
        _button = GetComponent<Button>();
        if (_button == null) Debug.LogError("Button не найден на объекте!");
    }

    private void OnEnable()
    {
        if(_button != null)
            _button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        if(_button != null)
            _button.onClick.RemoveListener(OnClick);
    }

    public void OnClick()
    {
        Debug.Log("click");
        
        if(localPlayer != null)
            localPlayer.SelectChoice(_choiseIndex);
        else
            Debug.LogWarning("localPlayer не назначен!");
    }
}
