using UnityEngine;

public class LogoScene : MonoBehaviour {
    [Header("Place this script into the main logo image")]
    [SerializeField] private float duration = 2.0f;
    [SerializeField] private int nextSceneIndex = 1;
    [SerializeField] private int tutorialSceneIndex = 3;

    //private void Awake() {
    //    Time.timeScale = 1f;
    //}

    //private void Start() {
    //    ShowNextScene();
    //}


    private void ShowNextScene() {
        if (GameResources.Instance.TutorialSytemData.FinishTutorialIntroduce) {
            SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.Home, onFadeOut: () =>
             Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnLoadHomeScene));
        }
        else {
            SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.Tutorial);
        }
        PlayerStatManager.Instance.Preload();
    }
}
