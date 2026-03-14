using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public static LoadingScreenManager Instance { get; private set; }
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private GameObject _loadingEndPos;
    [SerializeField] private GameObject _loadingStartPos;

    private const float CLOSE_ANIMATION_TIME = 0.25f;
    private const float OPEN_ANIMATION_TIME = 1.5f;
    private const float ARTIFICIAL_LOAD_TIME = 0.75f;

    private Vector3 _startPos;
    private Vector3 _endPos;

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
        DOTween.Kill(_loadingScreen.transform);
        StartCoroutine(DelayedFinishedAnimation());
    }

    public void PlayLoadAnimation(string nextScene)
    {
        if (_loadingScreen == null) return;
        _loadingScreen.gameObject.transform.DOKill();
        _loadingScreen.gameObject.transform.DOMove(_loadingStartPos.transform.position, CLOSE_ANIMATION_TIME).OnComplete(() =>
        {
            StartCoroutine(LoadSceneAndFinish(nextScene));
        });
    }

    private IEnumerator LoadSceneAndFinish(string nextScene)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(nextScene);
        yield return new WaitUntil(() => loading.isDone);
        yield return new WaitForSeconds(ARTIFICIAL_LOAD_TIME);
        PlayFinishedAnimation();
    }

    public void PlayFinishedAnimation()
    {
        if (_loadingScreen == null) return;
        _loadingScreen.gameObject.transform.DOKill();
        _loadingScreen.gameObject.transform.DOMove(_loadingEndPos.transform.position, OPEN_ANIMATION_TIME).OnComplete(() =>
        {
            OnLoadingScreenFinished?.Invoke();
        });
    }

    private IEnumerator DelayedFinishedAnimation()
    {
        yield return null;
        PlayFinishedAnimation();
    }
}
