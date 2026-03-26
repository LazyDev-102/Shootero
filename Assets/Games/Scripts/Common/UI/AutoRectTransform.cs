using System.Collections;
using UnityEngine;
using GameSystem.Common.UnityInspector;

namespace GameSystem.Common.UI {
    [RequireComponent(typeof(RectTransform))]
    public class AutoRectTransform : MonoBehaviour {

        [System.Serializable]
        private struct RectTransformCustom {
            public Vector2 aspectRatioRange;
            public Vector2 anchorMin;
            public Vector2 anchorMax;
            public Vector2 anchoredPosition;
            public Vector2 sizeDelta;
            public Vector2 pivot;
        }

        [SerializeField] private bool autoRect = true;
        [SerializeField] private float updateRate = 1f;
        [SerializeField] private RectTransformCustom[] customs;

        private RectTransform rectTransform;
        private WaitForSeconds wait;
        private float currentAspectRatio;

        private void Awake() {
            rectTransform = transform as RectTransform;
            wait = new WaitForSeconds(updateRate);
        }

        private void OnEnable() {
            if (autoRect) {
                StartCoroutine(AutoRect());
            } else {
                CalculatorRectTransform();
            }
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        private IEnumerator AutoRect() {
            while (true) {
                CalculatorRectTransform();
                yield return wait;
            }
        }

        [ContextMenu("Calculator")]
        private void CalculatorRectTransform() {
            Camera camera = Camera.main;
            if (camera == null)
                return;

            float aspectRatio = camera.aspect;
            if (currentAspectRatio == aspectRatio)
                return;

            currentAspectRatio = aspectRatio;

            foreach (RectTransformCustom custom in customs) {
                if (currentAspectRatio >= custom.aspectRatioRange.x && currentAspectRatio <= custom.aspectRatioRange.y) {
                    rectTransform.pivot = custom.pivot;
                    rectTransform.anchorMin = custom.anchorMin;
                    rectTransform.anchorMax = custom.anchorMax;
                    rectTransform.sizeDelta = custom.sizeDelta;
                    rectTransform.anchoredPosition = custom.anchoredPosition;
                    break;
                }
            }
        }

        [ContextMenu("Add")]
        private void Add() {
            RectTransform rectTransform = transform as RectTransform;
            System.Array.Resize(ref customs, customs.Length + 1);
            customs[customs.Length - 1] = new RectTransformCustom() {
                aspectRatioRange = Vector2.zero,
                anchorMin = rectTransform.anchorMin,
                anchorMax = rectTransform.anchorMax,
                anchoredPosition = rectTransform.anchoredPosition,
                sizeDelta = rectTransform.sizeDelta,
                pivot = rectTransform.pivot
            };
        }
    }
}

