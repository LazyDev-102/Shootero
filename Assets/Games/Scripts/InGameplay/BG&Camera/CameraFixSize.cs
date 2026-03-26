using UnityEngine;

public class CameraFixSize : MonoBehaviour {
    private void Awake() {
        Application.targetFrameRate = 60;
        GetComponent<Camera>().orthographicSize = Screen.height * 0.5f * (ConfigIngameData.borderW / Screen.width);
        ConfigIngameData.borderH = 1.0f * Screen.height / Screen.width * ConfigIngameData.borderW;
    }
}
