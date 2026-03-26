using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helper;
using System;

public class PlayerTakeHitEffect : MonoBehaviour {

    [SerializeField] private Image imgDamageSign;
    [SerializeField] private Image imgDamageSignConfig;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDurationOnWeak = 1f;

    private Tween currentFadeTween;
    private Tween currentFadeTweenConfig;
    private Tween currentFadeTweenConfigAll;
    public void ShowFade() {
        imgDamageSign.ChangeAlpha(0);
        //IngameHUD.Instance.GetFrame<CombatPanel>().ShakePlayerHealthBar(fadeDuration);
        currentFadeTween = imgDamageSign.DOFade(1, fadeDuration / 2).SetEase(Ease.OutBack).OnComplete(() => {
            HideFade();
        });
        ;
    }

    private void HideFade() {
        imgDamageSign.ChangeAlpha(1);
        currentFadeTween = imgDamageSign.DOFade(0, fadeDuration * 2).SetEase(Ease.Linear);
    }
    public void ShowFade(float startValue, float endValue) {
        imgDamageSignConfig.ChangeAlpha(startValue);
        //IngameHUD.Instance.GetFrame<CombatPanel>().ShakePlayerHealthBar(fadeDuration);
        var update = false;
        currentFadeTweenConfigAll = transform.DOScaleX(1, fadeDurationOnWeak * 2).OnUpdate(() => {
            if (!update) {
                update = true;
                currentFadeTweenConfig = imgDamageSignConfig.DOFade(endValue, fadeDurationOnWeak).SetEase(Ease.OutBack).OnComplete(() => {
                    HideFade(startValue, endValue, () => { update = false; });
                });
            }
        }).SetLoops(-1, LoopType.Restart);
    }

    private void HideFade(float startValue, float endValue, Action onComplete) {
        imgDamageSignConfig.ChangeAlpha(endValue);
        currentFadeTweenConfig = imgDamageSignConfig.DOFade(startValue, fadeDurationOnWeak).SetEase(Ease.Linear).OnComplete(() => {
            onComplete?.Invoke();
        });
    }
    public void StopShowFade() {
        currentFadeTween.Kill(true);
    }
    public void StopShowFadeConfig() {
        currentFadeTweenConfig.Kill(true);
        currentFadeTweenConfigAll.Kill(true);
    }
}
