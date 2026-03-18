using Unity.Netcode;
using UnityEngine;

public class NeyworkGameManager : MonoBehaviour
{
    public Transform[] spawnPoints; // назначаем в инспекторе
    public GameObject playerPrefab;

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        // На сервере спавним игрока
        if (NetworkManager.Singleton.IsServer)
        {
            // Берём свободную точку спавна
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject playerObj = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            playerObj.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId, true);
        }
    }
}
