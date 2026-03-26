
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Helper;
using System;
using TMPro;

public class ModesBuffEffect : MonoBehaviour {

    [SerializeField] private Image imgBuff;
    [SerializeField] private Color buffSprite;
    [SerializeField] private Color debuffSprite;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeDurationOnWeak = 1f;
    [SerializeField] private TextMeshProUGUI buffDescription;
    [SerializeField] private Image buffFrame;

    private Tween currentFadeTween;
    private Tween currentFadeTweenConfig;
    private Tween currentFadeTweenConfigAll;

    public Image BuffFrame { get => buffFrame; }

    public void ShowFade(bool isBuff, string description) {
        imgBuff.color = isBuff ? buffSprite : debuffSprite;
        buffFrame.color = isBuff ? buffSprite : debuffSprite;
        buffFrame.SetAlpha(1);
        buffDescription.text = description;
        imgBuff.ChangeAlpha(0);
        currentFadeTween = imgBuff.DOFade(1, fadeDuration / 2).SetEase(Ease.OutBack).OnComplete(() => {
            HideFade();
        });
    }

    private void HideFade() {
        imgBuff.ChangeAlpha(1);
        currentFadeTween = imgBuff.DOFade(0, fadeDuration * 2).SetEase(Ease.Linear);
    }
    public void ShowFade(bool isBuff, string description, float startValue, float endValue) {
        imgBuff.color = isBuff ? buffSprite : debuffSprite;
        buffFrame.color = isBuff ? buffSprite : debuffSprite;
        buffFrame.SetAlpha(1);
        buffDescription.text = description;
        imgBuff.ChangeAlpha(startValue);
        var update = false;
        currentFadeTweenConfigAll = transform.DOScaleX(1, fadeDurationOnWeak * 2).OnUpdate(() => {
            if (!update) {
                update = true;
                currentFadeTweenConfig = imgBuff.DOFade(endValue, fadeDurationOnWeak).SetEase(Ease.OutBack).OnComplete(() => {
                    HideFade(startValue, endValue, () => { update = false; });
                });
            }
        }).SetLoops(-1, LoopType.Restart);
    }

    private void HideFade(float startValue, float endValue, Action onComplete) {
        imgBuff.ChangeAlpha(endValue);
        currentFadeTweenConfig = imgBuff.DOFade(startValue, fadeDurationOnWeak).SetEase(Ease.Linear).OnComplete(() => {
            onComplete?.Invoke();
        });
    }
    public void StopShowFade() {
        currentFadeTween.Kill(true);
        imgBuff.SetAlpha(0);
    }
    public void StopShowFadeConfig() {
        currentFadeTweenConfig.Kill(true);
        currentFadeTweenConfigAll.Kill(true);
        //imgBuff.SetAlpha(0);
    }
}
