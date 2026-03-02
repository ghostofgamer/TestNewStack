using UnityEngine;
using Zenject;

public class PayTest : MonoBehaviour
{
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _wallet.Add(50);
    }
}