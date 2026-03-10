using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetReferenceHandler : MonoBehaviour
{
    [Header("Присвоить Addressable Prefab здесь")]
    public AssetReference myPrefabReference;

    private GameObject spawnedObject;
    private AsyncOperationHandle<GameObject> handle;

    // Загружаем и создаём объект
    public void LoadAsset()
    {
        if (myPrefabReference != null)
        {
            handle = myPrefabReference.InstantiateAsync(Vector3.zero, Quaternion.identity);
            handle.Completed += OnAssetLoaded;
        }
    }

    private void OnAssetLoaded(AsyncOperationHandle<GameObject> obj)
    {
        spawnedObject = obj.Result;
        Debug.Log("Asset Loaded!");
    }

    // Удаляем объект и освобождаем память
    public void ReleaseAsset()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
        }

        if (handle.IsValid())
        {
            Addressables.ReleaseInstance(handle);
        }
    }
}
