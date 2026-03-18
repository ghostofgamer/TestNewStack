using UnityEngine;
using Zenject;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private string _scene;
    
    private PersistentLoadingScreen _persistentLoadingScreen;

    [Inject]
    private void Construct(PersistentLoadingScreen persistentLoadingScreen)
    {
        _persistentLoadingScreen = persistentLoadingScreen;
    }

    public void Click()
    {
        _persistentLoadingScreen.LoadScene(_scene);
    }
}