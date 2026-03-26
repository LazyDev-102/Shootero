using UnityEngine;

public class DisableAllDebug : MonoBehaviour {
    [SerializeField] private bool enableOnEditor;
    [SerializeField] private bool enableOnAndroid;

    private void Awake() {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = enableOnEditor;
#else
        Debug.unityLogger.logEnabled = enableOnAndroid;
#endif
    }

}
