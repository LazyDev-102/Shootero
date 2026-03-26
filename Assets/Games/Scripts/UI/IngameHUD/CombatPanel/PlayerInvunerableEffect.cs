using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helper;
using System;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class PlayerInvunerableEffect : MonoBehaviour {

    [SerializeField] private Image imgHealthSignConfig;
    [SerializeField] private float startValue;
    [SerializeField] private float endValue;
    [SerializeField] private float timeDuration = 1;

    private Tween currentFadeTweenConfig;
    private float totalTime = 1;
    private bool isPlay;
    private TweenerCore<Vector3, Vector3, VectorOptions> tween;

    public void ShowFade(float totalTime) {
        if (isPlay)
            return;
        isPlay = true;
        this.totalTime = totalTime == -1 ? 1000 : totalTime;
        imgHealthSignConfig.ChangeAlpha(startValue);
        var update = false;
        var timePlay = 0f;
        tween = transform.DOScaleX(1, timeDuration).OnUpdate(() => {
            if (!update) {
                update = true;
                currentFadeTweenConfig = imgHealthSignConfig.DOFade(endValue, timeDuration / 2).SetEase(Ease.OutBack).OnComplete(() => {
                    HideFade(startValue, endValue, () => {
                        timePlay += timeDuration;
                        if (timePlay < this.totalTime) {
                            update = false;
                        }
                        else {
                            timePlay = 0f;
                            tween.Kill();
                        }
                    });
                });
            }
        }).SetLoops(-1, LoopType.Restart);
    }

    private void HideFade(float startValue, float endValue, Action onComplete) {
        imgHealthSignConfig.ChangeAlpha(endValue);
        currentFadeTweenConfig = imgHealthSignConfig.DOFade(startValue, timeDuration / 2).SetEase(Ease.Linear).OnComplete(() => {
            onComplete?.Invoke();
        });
    }
    public void StopShowFadeConfig() {
        currentFadeTweenConfig?.Kill(true);
        tween?.Kill(true);
        DOVirtual.DelayedCall(1f, () => imgHealthSignConfig.SetAlpha(0));
        isPlay = false;
    }
}
