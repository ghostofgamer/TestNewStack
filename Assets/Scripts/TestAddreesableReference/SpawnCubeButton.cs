using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class SpawnCubeButton : MonoBehaviour
{
    [Header("UI")] public Button spawnButton;
    public TMP_Text statusText;

    [Header("Addressable Key")] public string prefabAddress = "cube_remote"; // <-- точный ключ Addressable

    private void Start()
    {
        if (spawnButton != null)
            spawnButton.onClick.AddListener(StartLoading);
    }

    private void StartLoading()
    {
        // Запускаем корутину для загрузки конкретного префаба
        StartCoroutine(LoadPrefab(prefabAddress));
    }

    private System.Collections.IEnumerator LoadPrefab(string key)
    {
        if (statusText != null)
            statusText.text = "Starting load...";
        Debug.Log("Starting load for key: " + key);

        // Загружаем префаб
        AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(
            key,
            Vector3.zero,
            Quaternion.identity
        );

        // Отслеживаем прогресс
        while (!handle.IsDone)
        {
            float percent = handle.PercentComplete * 100f;
            string msg = $"Loading {key}: {percent:F1}%";
            Debug.Log(msg);
            if (statusText != null)
                statusText.text = msg;
            yield return null;
        }

        // Проверяем результат
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            GameObject prefab = handle.Result;
            prefab.transform.position = new Vector3(0, 1, 0); // ставим на сцене
            string successMsg = $"Prefab '{key}' loaded successfully!";
            Debug.Log(successMsg);
            if (statusText != null)
                statusText.text = successMsg;
        }
        else
        {
            string failMsg = $"Failed to load prefab with key '{key}'!";
            Debug.LogError(failMsg);
            if (statusText != null)
                statusText.text = failMsg;
        }
    }

    private void OnDestroy()
    {
        if (spawnButton != null)
            spawnButton.onClick.RemoveAllListeners();
    }

    /*private void Start()
    {
       if (spawnButton != null)
          spawnButton.onClick.AddListener(() => SpawnCubeAsync().Forget());
    }

    private async UniTaskVoid SpawnCubeAsync()
    {
       // Шаг 1: подготовка
       statusText.text = "Step 1: Preparing to load cube...";
       Debug.Log(statusText.text);
       await UniTask.Delay(500);

       // Шаг 2: очищаем кэш для принудительного скачивания с сервера
       statusText.text = "Step 2: Clearing cache...";
       Debug.Log(statusText.text);
       AsyncOperationHandle clearHandle = Addressables.ClearDependencyCacheAsync(cubeAddress);


       await UniTask.Delay(500);

       // Шаг 3: старт загрузки
       statusText.text = "Step 3: Starting download from Remote...";
       Debug.Log(statusText.text);

       // Загружаем префаб
       var handle = Addressables.InstantiateAsync(cubeAddress, Vector3.zero, Quaternion.identity);

       // Отслеживаем прогресс
       while (!handle.IsDone)
       {
          string progressMsg = $"Loading... {handle.PercentComplete * 100f:F1}%";
          statusText.text = progressMsg;
          Debug.Log(progressMsg);
          await UniTask.Yield();
       }

       // Проверяем результат
       if (handle.Status == AsyncOperationStatus.Succeeded)
       {
          GameObject cube = handle.Result;
          cube.transform.position = new Vector3(0, 1, 0);

          string successMsg = "Step 4: Cube loaded successfully from server/cache!";
          statusText.text = successMsg;
          Debug.Log(successMsg);
       }
       else
       {
          string failMsg = "Step 4: Failed to load cube!";
          statusText.text = failMsg;
          Debug.LogError(failMsg);
       }

       await UniTask.Delay(500);
       statusText.text = "Ready.";
       Debug.Log("Ready.");
    }

    private void OnDestroy()
    {
       if (spawnButton != null)
          spawnButton.onClick.RemoveAllListeners();
    }*/
}