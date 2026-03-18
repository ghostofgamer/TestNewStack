using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestLoadScenes : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    
    [SerializeField]private FadeScreen _fadeScreen;
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneRoutine(sceneName));
    }

    /*IEnumerator Load(string sceneName)
    {
        loadingScreen.SetActive(true);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float fakeProgress = 0f;

        while (op.progress < 0.9f)
        {
            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
            fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, Time.deltaTime);
            progressBar.value = fakeProgress;
            yield return null;
        }
        
        progressBar.value = 1f;
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeScreen.FadeOut());
        op.allowSceneActivation = true;
    }*/
    
    [Header("Settings")]
    public float minLoadTime = 0.5f; 
    
    private IEnumerator LoadSceneRoutine(string sceneName)
    {
        loadingScreen.SetActive(true);

        // Начало загрузки новой сцены
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        float fakeProgress = 0f;
        float elapsed = 0f;

        // Обновляем прогресс до 0.9 (Unity)
        while (op.progress < 0.9f || elapsed < minLoadTime)
        {
            elapsed += Time.deltaTime;

            float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
            fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, Time.deltaTime);
            if (progressBar != null)
                progressBar.value = fakeProgress;

            yield return null;
        }

        progressBar.value = 1f; // прогресс достиг конца
        yield return StartCoroutine(_fadeScreen.FadeOut());

        // После того как экран полностью черный — активируем сцену
        op.allowSceneActivation = true;
    }
}