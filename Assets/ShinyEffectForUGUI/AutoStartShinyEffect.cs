using Coffee.UIExtensions;
using DG.Tweening;
using UnityEngine;

public class AutoStartShinyEffect : MonoBehaviour {
    [SerializeField] ShinyEffectForUGUI shinyEffect;
    [SerializeField] private float duration;
    [SerializeField] private float deltaCall;

    Tween curTween;

    private void OnEnable() {
        if (curTween != null) {
            curTween.Kill();
            shinyEffect.ResetLocation(0);
        }
        curTween = DOVirtual.DelayedCall(deltaCall, () => {
            shinyEffect.Play(duration);
        }).SetLoops(-1);
    }

    private void OnDisable() {
        if (curTween != null) {
            curTween.Kill();
        }
    }
}
