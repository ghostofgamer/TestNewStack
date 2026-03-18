using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersistentLoadingScreen : MonoBehaviour
{
    [Header("UI Elements")]
    public CanvasGroup canvasGroup;
    public Slider progressBar;

    [Header("Settings")]
    public float fadeSpeed = 3f;      
    public float progressSpeed = 1.5f; 
    public float minLoadTime = 0.5f;  

    private Coroutine _loadingCoroutine;
    private bool _isLoading = false;
    
    private CancellationTokenSource _cts;
    
    private void OnDisable()
    {
        // Прерываем задачу при деактивации объекта
        _cts?.Cancel();
    }
   
    public void LoadScene(string sceneName)
    {
        progressBar.value = 0;
        
        if (_isLoading) return;
        
        _cts?.Cancel();
        _cts = new CancellationTokenSource();
        
        Load(sceneName,_cts.Token).Forget();
    }
    
    private async UniTask Load(string sceneName, CancellationToken ct)
    {
        if (_isLoading) return;
        _isLoading = true;

        try
        {
            /*// FadeIn с токеном отмены
            await FadeIn(ct);

            // Начинаем загрузку сцены
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
            op.allowSceneActivation = false;

            float fakeProgress = 0f;
            float elapsed = 0f;

            while (op.progress < 0.9f || elapsed < minLoadTime)
            {
                ct.ThrowIfCancellationRequested();

                elapsed += Time.deltaTime;
                float targetProgress = Mathf.Clamp01(op.progress / 0.9f);
                fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, progressSpeed * Time.deltaTime);

                if (progressBar != null)
                    progressBar.value = fakeProgress;

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            if (progressBar != null)
                progressBar.value = 1f;

            await UniTask.Delay(System.TimeSpan.FromSeconds(0.2f), cancellationToken: ct);

            // Разрешаем активацию сцены
            op.allowSceneActivation = true;
            await op.ToUniTask(cancellationToken: ct);

            // FadeOut
            await FadeOut(ct);*/
            
            await FadeIn(ct);

            // =============================
            // ✅ ЗАМЕНЕНО: Используем Addressables вместо SceneManager
            // =============================
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, activateOnLoad: false);

            float fakeProgress = 0f;
            float elapsed = 0f;

            while (!handle.IsDone || elapsed < minLoadTime)
            {
                ct.ThrowIfCancellationRequested();

                elapsed += Time.deltaTime;

                // ✅ Используем handle.PercentComplete вместо op.progress
                float targetProgress = Mathf.Clamp01(handle.PercentComplete);
                fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, progressSpeed * Time.deltaTime);

                if (progressBar != null)
                    progressBar.value = fakeProgress;

                await UniTask.Yield(PlayerLoopTiming.Update, ct);
            }

            // ✅ Активируем сцену после загрузки
            await handle.Task;
            handle.Result.ActivateAsync();
            
            if (progressBar != null)
                progressBar.value = 1f;

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: ct);

            await FadeOut(ct);
        }
        catch (OperationCanceledException)
        {
            // Загрузка была отменена — безопасно выходим
            Debug.Log("Загрузка сцены отменена");
        }
        catch (Exception ex)
        {
            Debug.LogError("Ошибка: " + ex.Message + "\n" + ex.StackTrace);
            Debug.Log("Произошла ошибка: " + ex.Message);
        }
        finally
        {
            _isLoading = false; // флаг сбрасываем всегда
        }
    }
    
    private async UniTask FadeIn(CancellationToken ct)
    {
        gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1f)
        {
            ct.ThrowIfCancellationRequested();

            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        canvasGroup.alpha = 1f;
    }

    private async UniTask FadeOut(CancellationToken ct)
    {
        while (canvasGroup.alpha > 0f)
        {
            ct.ThrowIfCancellationRequested();

            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            await UniTask.Yield(PlayerLoopTiming.Update, ct);
        }

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }
    
    
    
    /*private async UniTask FadeIn()
    {
        gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;

        while (canvasGroup.alpha < 1f)
        {
            canvasGroup.alpha += Time.deltaTime * fadeSpeed;
            await UniTask.Yield();
        }

        canvasGroup.alpha = 1f;
    }
    
    private async UniTask FadeOut()
    {
        while (canvasGroup.alpha > 0f)
        {
            canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
            await UniTask.Yield();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }*/

    /*// Внешний вызов загрузки сцены
    public void Load(string sceneName)
    {
        Debug.Log("Loading Scene: " + sceneName);
        
        gameObject.SetActive(true);
        
        if (_loadingCoroutine != null)
            StopCoroutine(_loadingCoroutine);

        _loadingCoroutine = StartCoroutine(LoadSceneRoutine(sceneName));
    }*/

    /*private IEnumerator LoadSceneRoutine(string sceneName)
    {
        progressBar.value = 0;
        // Плавное появление окна
        yield return StartCoroutine(FadeIn());

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
            fakeProgress = Mathf.MoveTowards(fakeProgress, targetProgress, progressSpeed * Time.deltaTime);

            if (progressBar != null)
                progressBar.value = fakeProgress;

            yield return null;
        }

        // Прогресс достиг 100%
        if (progressBar != null)
            progressBar.value = 1f;

        // Небольшая задержка для красоты (можно убрать или изменить)
        yield return new WaitForSeconds(0.2f);

        // Разрешаем активацию сцены
        op.allowSceneActivation = true;

        // Ждём пока сцена полностью активируется
        while (!op.isDone)
            yield return null;

        // Плавное скрытие окна
        yield return StartCoroutine(FadeOut());
    }*/
}