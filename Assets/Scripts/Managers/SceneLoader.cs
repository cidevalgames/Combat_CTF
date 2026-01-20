using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Eflatun.SceneReference;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private SceneReference[] gameScenes;

    public event Action OnLoadingDoneEvent;
    public event Action OnUnloadingDoneEvent;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            return;
    }

    private void Start()
    {
#if !UNITY_EDITOR
        LoadGame();    
#else
        ReloadGame();
#endif
    }

    [ContextMenu("Load game")]
    public void LoadGame()
    {
        StartCoroutine(LoadScenesList(gameScenes));
    }

    [ContextMenu("Unload game")]
    public void UnloadGame()
    {
        StartCoroutine(UnloadScenesList(gameScenes));
    }

    [ContextMenu("Reload game")]
    public void ReloadGame()
    {
        StartCoroutine(ReloadScenesList(gameScenes));
    }

    private IEnumerator LoadScenesList(SceneReference[] list)
    {
        foreach (var scene in list)
        {
            var asyncOp = SceneManager.LoadSceneAsync(scene.Name, LoadSceneMode.Additive);

            while (!asyncOp.isDone)
                yield return null;
        }

        OnLoadingDoneEvent?.Invoke();
        OnLoadingDoneEvent = null;
    }

    public IEnumerator ReloadScenesList(SceneReference[] list)
    {
        yield return UnloadScenesList(list);
        yield return LoadScenesList(list);
    }

    private IEnumerator UnloadScenesList(SceneReference[] list)
    {
        foreach (var scene in list)
        {
            var asyncOp = SceneManager.UnloadSceneAsync(scene.Name);

            while (!asyncOp.isDone)
                yield return null;
        }

        OnUnloadingDoneEvent?.Invoke();
        OnUnloadingDoneEvent = null;
    }
}
