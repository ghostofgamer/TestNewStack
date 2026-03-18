using Unity.Netcode;
using UnityEngine;

public class PlayerRPS : NetworkBehaviour
{
    public SpriteRenderer choiceSprite; // спрайт над головой
    public Sprite rockSprite;
    public Sprite paperSprite;
    public Sprite scissorsSprite;

    private NetworkVariable<int> choice = new NetworkVariable<int>(-1); // -1 = нет выбора
    
    public static PlayerRPS LocalPlayer { get; private set; }
    
    public override void OnNetworkSpawn()
    {
        if (IsOwner) // локальный игрок
        {
            LocalPlayer = this;

            // уведомляем UIManager, что локальный игрок готов
            UIManagerInit.Instance.SetLocalPlayer(this);
        }

        choice.OnValueChanged += OnChoiceChanged;
    }

    
    /*private void Start()
    {
        choice.OnValueChanged += OnChoiceChanged;
    }*/

    void OnChoiceChanged(int oldValue, int newValue)
    {
        switch(newValue)
        {
            case 0: choiceSprite.sprite = rockSprite; break;
            case 1: choiceSprite.sprite = scissorsSprite; break;
            case 2: choiceSprite.sprite = paperSprite; break;
            default: choiceSprite.sprite = null; break;
        }
    }

    public void SelectChoice(int selection)
    {
        if(!IsOwner) return; // только владелец игрока может выбрать
        MakeChoiceServerRpc(selection);
    }

    [ServerRpc]
    void MakeChoiceServerRpc(int selection)
    {
        
        choice.Value = selection; // синхронизируется с клиентами
        Debug.Log(selection);
    }
}
