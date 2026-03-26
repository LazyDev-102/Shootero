using DG.Tweening;
using GameAnalyticsSDK.Setup;
using Gemmob;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : ProgressFillAmountBase {
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showAnimation;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideAnimation;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation fadeInAnimation;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation fadeOutAnimation;
    [SerializeField] private Image effect;
    [SerializeField] private Transform highlight;
    [SerializeField] private float offsetExpVirtualTime = 0.7f;
    [SerializeField] private float onePiece = 0.015f;
    [SerializeField] private float timeFadePiece = 1.2f;
    private float oldPct = 1;
    private Vector3 oldExpVirtual;
    private float expMaxSize;
    protected override void Assign() {
        base.Assign();
        oldExpVirtual = effect.transform.position;
        expMaxSize = IngameHUD.Instance.transform.parent.rectTransform().sizeDelta.x + transform.rectTransform().sizeDelta.x;
    }
    public void AddListenerHealthChanged(EnemyBase boss) {
        boss.EnemyHealth.AddOnHpChanged(HandleBossHealthChanged);
    }

    public void RemoveListenerHealthChanged(EnemyBase boss) {
        boss.EnemyHealth.RemoveOnHpChanged(HandleBossHealthChanged);
    }

    private void HandleBossHealthChanged(int health, float pct) {
        if (GameManager.Instance.isTest)
            return;
        bool decrease = pct < oldPct;
        HandleBarChanged(pct);
        ChangeSpeed(decrease);
        PlayEffect(decrease, pct);
        ChangeOldPct(pct);
    }
    private void ChangeSpeed(bool decrease) {
        speed = decrease ? originSpeed : 1;
        if (speed == 0) {
            originSpeed = 1;
            speed = 1;
        }
    }
    private void ChangeOldPct(float value) {
        oldPct = value;
    }
    private void PlayEffect(bool status, float pct) {
        if (!status)
            return;
        var maxLength = (pct - 1f) * expMaxSize;
        oldExpVirtual = new Vector3(maxLength, 0, 0);
        if (highlight.rectTransform() != null)
            highlight.rectTransform().anchoredPosition = oldExpVirtual;
        oldExpVirtual.x += 5f;
        highlight.gameObject.SetActive(false);
        highlight.gameObject.SetActive(true);
        int length = (int)((oldPct - pct) / onePiece);
        if (length < 1)
            length = 1;
        for (int i = 0; i < length; i++) {
            var exp = effect.Spawn(processImage.transform);
            exp.transform.localPosition = effect.transform.localPosition;
            exp.rectTransform.anchoredPosition = oldExpVirtual;
            exp.gameObject.SetActive(true);
            exp.SetAlpha(1);
            exp.DOFade(0, timeFadePiece);
            exp.rectTransform.DOAnchorPosY(-100, offsetExpVirtualTime - i * onePiece).SetEase(Ease.InQuint).OnComplete(() => {
                DOVirtual.DelayedCall(2f, () => exp.Recycle());
                highlight.gameObject.SetActive(false);
            });
            oldExpVirtual.x += effect.rectTransform.sizeDelta.x;
        }
    }
    public void Show(Action onComplete) {
        if (showAnimation) {
            showAnimation.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    public void Hide(Action onComplete) {
        if (hideAnimation) {
            hideAnimation.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    public void FadeIn(Action onComplete) {
        if (fadeInAnimation) {
            fadeInAnimation.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }

    public void FadeOut(Action onComplete) {
        if (fadeOutAnimation) {
            fadeOutAnimation.Play(onComplete, true);
        }
        else {
            onComplete?.Invoke();
        }
    }
}
