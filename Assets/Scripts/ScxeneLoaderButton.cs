using UnityEngine;
using UnityEngine.SceneManagement;


public class ScxeneLoaderButton : MonoBehaviour
{
    [SerializeField] private string _nameScene;
    
    public void SceneLoad()
    {
       SceneManager.LoadScene(_nameScene);
    }
}
