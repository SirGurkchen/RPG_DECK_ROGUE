using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles the Loading Screen of the game.
/// Plays the loading screen animation and loads new scenes.
/// </summary>
public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance { get; private set; }
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _loadingEndPos;

    private const float CLOSE_ANIMATION_TIME = 0.25f;
    private const float OPEN_ANIMATION_TIME = 1.5f;
    private const float ARTIFICIAL_LOAD_TIME = 0.75f;

    private Vector3 _startPos;

    public event Action OnLoadingScreenFinished;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        _startPos = _loadingScreen.transform.position;
        DOTween.Kill(_loadingScreen.transform);
    }

    /// <summary>
    /// Plays the loading animation and loads given next scene.
    /// </summary>
    /// <param name="nextScene">Name of next scene.</param>
    public void PlayLoadAnimation(string nextScene)
    {
        if (_loadingScreen == null) return;
        _loadingScreen.gameObject.transform.DOKill();
        AudioManager.Instance.PlayChangeSound();
        _loadingScreen.gameObject.transform.DOMove(_loadingEndPos.transform.position, CLOSE_ANIMATION_TIME)
            .SetUpdate(true)
            .OnComplete(() => StartCoroutine(LoadSceneAndFinish(nextScene)));
    }

    private IEnumerator LoadSceneAndFinish(string nextScene)
    {
        Time.timeScale = 1f;
        AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
        yield return new WaitUntil(() => loading.isDone);
        yield return new WaitForSeconds(ARTIFICIAL_LOAD_TIME);
        PlayFinishedAnimation();
    }

    /// <summary>
    /// Plays the loading finished animation.
    /// </summary>
    public void PlayFinishedAnimation()
    {
        if (_loadingScreen == null) return;
        _loadingScreen.gameObject.transform.DOKill();
        _loadingScreen.gameObject.transform.DOMove(_startPos, OPEN_ANIMATION_TIME).OnComplete(() =>
        {
            OnLoadingScreenFinished?.Invoke();
        });
    }
}
