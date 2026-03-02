using TMPro;
using UnityEngine;
using Zenject;

public class WalletViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
        _wallet.OnBalanceChanged += Show;
        Show(_wallet.Balance);
    }

    private void Show(int amount)
    {
        _text.text = amount.ToString();
    }
}