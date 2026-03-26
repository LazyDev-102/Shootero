using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using TMPro;
using Gemmob;
using DG.Tweening;

public class SceneLoader : SingletonBind<SceneLoader> {
    [Header("Home")]
    [SerializeField] private Image fade;
    [SerializeField] private GameObject background;
    [SerializeField] private Image logoIcon;
    [SerializeField] private TextMeshProUGUI loadingContent;
    [SerializeField] private float duration = 0.1f;
    [Header("GamePlay")]
    [SerializeField] private Image ingameIcon;
    [SerializeField] private Image ingameBackground;
    [SerializeField] private TextMeshProUGUI ingameLoadingContent;
    [SerializeField] private float ingameDuration = 2;

    private Coroutine loading;

    public void SetLoadingContent(string content) {
        if (loadingContent) {
            loadingContent.text = content;
        }
    }

    public void SetBackgroundState(bool show) {
        if (background) {
            background.SetActive(show);
        }
    }

    public void SetIngameBackgroundState(bool show) {
        if (ingameBackground) {
            ingameBackground.gameObject.SetActive(show);
        }
        fade.gameObject.SetActive(!show);
    }


    public void SetIngameIcon(bool show) {
        if (ingameIcon != null) {
            ingameIcon.gameObject.SetActive(show);
        }
        if (ingameLoadingContent) {
            ingameLoadingContent.gameObject.SetActive(show);
        }
    }

    public void LoadSceneAsyn(string sceneName, Action onFadeIn = null, Action onFadeOut = null, string loadingContent = "Loading...") {
        if (loading != null) {
            StopCoroutine(loading);
        }

        loading = StartCoroutine(DoLoadSceneAsyn(
            () => SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single)
            , duration
            , onFadeIn
            , onFadeOut
            , loadingContent));
    }

    public void LoadSceneAsyn(string sceneName, float duration, Action onFadeIn = null, Action onFadeOut = null, string loadingContent = "Loading...") {
        if (loading != null) {
            StopCoroutine(loading);
        }

        loading = StartCoroutine(DoLoadSceneAsyn(
            () => SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single)
            , duration
            , onFadeIn
            , onFadeOut
            , loadingContent));
    }


    public void LoadSceneAsyn(int sceneIndex, Action onFadeIn = null, Action onFadeOut = null, string loadingContent = "Loading...", bool preLoad = true) {
        LoadSceneAsyn(sceneIndex, duration, onFadeIn, onFadeOut, loadingContent, preLoad);
    }

    public void LoadSceneAsyn(int sceneIndex, float duration, Action onFadeIn = null, Action onFadeOut = null, string loadingContent = "Loading...", bool preLoad = true) {
        if (loading != null) {
            StopCoroutine(loading);
        }
        if (sceneIndex == (int)SceneDefined.Index.GamePlay) {
            Action newOnFadeOut = onFadeOut;

            newOnFadeOut = () => {
                if (preLoad)
                    GameResources.Instance.PreloadGame.PreloadIngame();
                onFadeOut?.Invoke();
            };
            loading = StartCoroutine(DoLoadInGameSceneAsyn(
              () => SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single)
              , ingameDuration
              , onFadeIn
              , newOnFadeOut
              , loadingContent));
        }
        else if (sceneIndex == (int)SceneDefined.Index.Tutorial) {
            Action newonFadeOut = onFadeOut;

            newonFadeOut = () => {
                if (preLoad)
                    GameResources.Instance.PreloadGame.PreloadIngame();
                onFadeOut?.Invoke();
            };
            loading = StartCoroutine(DoLoadSceneAsyn(
                null
                , duration
                , onFadeIn
                , newonFadeOut
                , loadingContent
                , () => {
                    loading = StartCoroutine(DoLoadInGameSceneAsyn(
                    () => SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single)
                    , ingameDuration
                    , onFadeIn
                    , onFadeOut
                    , loadingContent));
                }));
        }
        else {
            loading = StartCoroutine(DoLoadSceneAsyn(
                () => SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Single)
                , duration
                , onFadeIn
                , onFadeOut
                , loadingContent));
        }
    }

    private IEnumerator DoLoadSceneAsyn(Func<AsyncOperation> loader, float duration, Action onFadeIn = null, Action onFadeOut = null, string loadingContent = "Loading...", Action onComplete = null) {
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        yield return StartCoroutine(DoFade(fade, 1, 0.15f));
        SetIngameBackgroundState(false);
        SetBackgroundState(true);
        SetLoadingContent(loadingContent);
        onFadeIn?.Invoke();
        yield return StartCoroutine(DoFade(fade, 0, 0.15f));

        AsyncOperation operation = loader?.Invoke();

        if (operation != null) {
            operation.allowSceneActivation = false;
            float elpased = 0f;

            while (!operation.isDone) {
                if (operation.progress >= 0.9f) {
                    yield return Yielder.Wait(duration - elpased);
                    operation.allowSceneActivation = true;
                }
                elpased += Time.deltaTime;
                yield return null;
            }
        }
        Resources.UnloadUnusedAssets();
        yield return StartCoroutine(DoFade(fade, 1, 0.15f));
        SetBackgroundState(false);
        onFadeOut?.Invoke();
        GC.Collect();
        onComplete?.Invoke();
        yield return StartCoroutine(DoFade(fade, 0, 0.15f));
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);
        loading = null;
    }
    private IEnumerator DoLoadInGameSceneAsyn(Func<AsyncOperation> loader, float duration, Action onFadeIn = null, Action onFadeOut = null, string loadingContent = "LOADING") {
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        fade.SetAlpha(0);
        ingameIcon.SetAlpha(0);
        SetBackgroundState(false);
        SetIngameBackgroundState(true);
        onFadeIn?.Invoke();
        SetIngameIcon(true);
        DoFadeLoadingText(0.6f);
        ingameIcon.DOFade(1, 0.3f);
        yield return StartCoroutine(DoFade(ingameBackground, 1, 0.2f, null));

        AsyncOperation operation = loader?.Invoke();

        if (operation != null) {
            operation.allowSceneActivation = false;
            float elpased = 0f;

            while (!operation.isDone) {
                if (operation.progress >= 0.9f) {
                    yield return Yielder.Wait(duration - elpased);
                    operation.allowSceneActivation = true;
                }
                elpased += Time.deltaTime;
                yield return null;
            }
        }
        Resources.UnloadUnusedAssets();
        onFadeOut?.Invoke();
        ingameLoadingContent.DOFade(0, 0.3f);
        ingameIcon.DOFade(0, 0.3f);
        yield return StartCoroutine(DoFade(ingameBackground, 0, 0.3f, () => SetIngameIcon(false)));
        SetIngameBackgroundState(false);
        GC.Collect();
        GameSystem.Common.UI.HUDManager.IgnoreUserInput(false);
        loading = null;
    }

    private IEnumerator DoFade(Graphic target, float endValue, float duration, Action onComplete = null, float delay = 0) {
        yield return Yielder.Wait(delay);

        float startValue = target.color.a;
        float elapsed = 0f;

        while (elapsed <= duration) {
            target.SetAlpha(Mathf.Lerp(startValue, endValue, elapsed / duration));
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.SetAlpha(endValue);

        onComplete?.Invoke();
    }
    private void DoFadeLoadingText(float duration, bool increase = true) {
        var update = false;
        if (increase) {
            ingameLoadingContent.transform.DOScaleY(1, duration).OnUpdate(() => {
                if (!update) {
                    update = true;
                    ingameLoadingContent.DOFade(0, duration / 3).SetEase(Ease.OutBack).OnComplete(() => {
                        ingameLoadingContent.DOFade(1, duration / 3 * 2).SetEase(Ease.Linear).OnComplete(() => { update = false; });
                    });
                }
            }).SetLoops(-1, LoopType.Restart);
        }
        else {
            ingameLoadingContent.transform.DOScaleY(1, duration).OnUpdate(() => {
                if (!update) {
                    update = true;
                    ingameLoadingContent.DOFade(1, duration / 3).SetEase(Ease.OutBack).OnComplete(() => {
                        ingameLoadingContent.DOFade(0, duration / 3 * 2).SetEase(Ease.Linear).OnComplete(() => { update = false; });
                    });
                }
            }).SetLoops(-1, LoopType.Restart);
        }
    }
    public void FakeScene() {
        //GameSystem.Common.UI.HUDManager.IgnoreUserInput(true);
        //StartCoroutine(DoFade(fade, 1, 0.15f));
        SetIngameBackgroundState(false);
        SetBackgroundState(true);
        SetLoadingContent("Loading");
    }

    public void LoadHomeScene(LoadSceneType type, Action onFadein = null, Action onFadeOut = null) {
        if (type == LoadSceneType.LoadAsyn)
            LoadSceneAsyn((int)SceneDefined.Index.Home, onFadeIn: () => {
                onFadein?.Invoke();
                var cScene = SceneManager.GetActiveScene().buildIndex;
                if (cScene == (int)SceneDefined.Index.GamePlay || cScene == (int)SceneDefined.Index.Tutorial) {
                    if (GameManager.Initialized) {
                        GameManager.Instance.GameLoader.DeSpawnAll();
                    }
                }
            }, onFadeOut: () => {
                onFadeOut?.Invoke();
                EventDispatcher.Instance.Dispatch(EventKey.OnLoadHomeScene);
                }) ;
        else
            SceneManager.LoadScene((int)SceneDefined.Index.Home);
    }
    public void LoadLogoScene(LoadSceneType type) {
        if (type == LoadSceneType.LoadAsyn)
            LoadSceneAsyn((int)SceneDefined.Index.Logo);
        else
            SceneManager.LoadScene((int)SceneDefined.Index.Logo);
    }
    public void LoadGamePlayScene(LoadSceneType type, Action onFadeIn = null) {
        SaveLoad.Save();
        if (type == LoadSceneType.LoadAsyn)
            LoadSceneAsyn((int)SceneDefined.Index.GamePlay, onFadeIn: onFadeIn, preLoad: GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce);
        else
            SceneManager.LoadScene((int)SceneDefined.Index.GamePlay);
        Tracking.Instance.LogMode(IngameData.currentGameMode);
    }
    public void LoadTutorialScene(LoadSceneType type) {
        if (type == LoadSceneType.LoadAsyn)
            LoadSceneAsyn((int)SceneDefined.Index.Tutorial);
        else
            SceneManager.LoadScene((int)SceneDefined.Index.Tutorial);
    }
}
