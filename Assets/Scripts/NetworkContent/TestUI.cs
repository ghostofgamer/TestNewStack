using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class TestUI : MonoBehaviour
{
    public void StartHostButtonClick()
    {
        // NetworkManager.Singleton.StartHost();
        
        var transport = NetworkManager.Singleton.GetComponent<UnityTransport>();

        // Берём случайный свободный порт (например, от 7777 до 8888)
        for (ushort port = 7777; port <= 8888; port++)
        {
            transport.SetConnectionData("127.0.0.1", port); // <-- здесь задаём порт для сервера
            try
            {
                NetworkManager.Singleton.StartHost();
                Debug.Log($"Host запущен на порту {port}");
                break; // успех, выходим из цикла
            }
            catch
            {
                // порт занят, пробуем следующий
            }
        }
    }

    public void StartClientButtonClick()
    {
        NetworkManager.Singleton.StartClient();
    }
}