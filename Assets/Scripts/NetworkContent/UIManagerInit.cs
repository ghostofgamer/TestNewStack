using Unity.Netcode;
using UnityEngine;

public class UIManagerInit : MonoBehaviour
{
    [SerializeField] private UIButtonChoise[] _uiButtonChoises; 
    public static UIManagerInit Instance;
    
    private void Awake()
    {
        Instance = this;
    }
    
    /*void Start()
    {
        // ждем, когда клиент подключится и объект локального игрока создан
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    void OnClientConnected(ulong clientId)
    {
        if(clientId == NetworkManager.Singleton.LocalClientId)
        {
            // получаем локального игрока
            var localPlayer = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject().GetComponent<PlayerRPS>();

            // находим все кнопки и присваиваем им localPlayer
            foreach(var btn in _uiButtonChoises)
            {
                btn.localPlayer = localPlayer;
            }
        }
    }*/
    
    public void SetLocalPlayer(PlayerRPS localPlayer)
    {
        foreach(var btn in _uiButtonChoises)
        {
            btn.localPlayer = localPlayer;
        }
    }
    
    
}
