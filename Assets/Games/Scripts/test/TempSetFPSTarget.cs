using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TempSetFPSTarget : MonoBehaviour {
    void Start() {
        Application.targetFrameRate = 60;
        fpsText.gameObject.SetActive(showFPS);
    }

    public TextMeshProUGUI fpsText;
    public float deltaTime;
    [SerializeField] private bool showFPS;

    void Update() {
        if (showFPS) {
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            float fps = 1.0f / deltaTime;
            fpsText.text = $"FPS:{Mathf.Ceil(fps)}";
        }
    }

}
