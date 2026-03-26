using UnityEngine;

public class DisableOnEditor : MonoBehaviour {
    [SerializeField] private bool turnOnEditor;

    void Start() {
#if UNITY_EDITOR
        gameObject.SetActive(turnOnEditor);
#else
        gameObject.SetActive(!turnOnEditor);
#endif
    }

}
