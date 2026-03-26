using GameSystem.Common.UI;
using UnityEngine;
using System;
using DG.Tweening;

public class AdsSpinPopup : DOTweenFrame {
    [SerializeField] private AdsSpinLayout spinLayout;
    [SerializeField] private BonusSpinLayout bonusLayout;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private Transform mainFrame;
    [Header("Boss")]
    [SerializeField] private RectTransform decoAppear;
    [SerializeField] private Transform adsSpinBoss;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform desPos;
    [SerializeField] private ParticleSystem effect;

    //[SerializeField] private 
    private AdsSpinData data;
    private Action onClose;
    private Vector2 decoSizeDelta = new Vector2(570, 625);
    private void Awake() {
        spinLayout.Assign(SpinLayoutDone);
        bonusLayout.Assign();
        backButton.AddEvent(OnClose);
    }
    private void ShowBoss(Action onCompleted) {
        SetShipEffectStatus(false);
        gameObject.SetActive(true);
        decoAppear.sizeDelta = Vector2.zero;
        decoAppear.DOSizeDelta(decoSizeDelta, 1f).OnComplete(() => {
            if (effect != null)
                effect.Play();
            DOVirtual.DelayedCall(0.5f, () => {
                adsSpinBoss.gameObject.SetActive(true);
                adsSpinBoss.localPosition = startPos.localPosition;
                adsSpinBoss.DOLocalMove(desPos.localPosition, 2f).OnComplete(() => onCompleted?.Invoke());
            });
        });
    }
    private void HideBoss(Action onCompleted) {
        if (hideAnimation != null)
            hideAnimation.Play();
        adsSpinBoss.DOLocalMove(startPos.localPosition, 1.5f).OnComplete(() => {
            if (effect != null)
                effect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            DOVirtual.DelayedCall(1f, () => {
                decoAppear.DOSizeDelta(Vector2.zero, 1f).OnComplete(() => {
                    onCompleted?.Invoke();
                    gameObject.SetActive(false);
                });
            });
        });
    }
    public void UpdateUI() {
        mainFrame.gameObject.SetActive(true);
        if (showAnimation != null)
            showAnimation.Play();
        data = GameResources.Instance.AdsSpin;
        spinLayout.UpdateUI(data.LuckyData, data.LuckyPercent);
        bonusLayout.UpdateUI(data.BonusData, data.BonusPercent);
        SetLayoutStatus(true);

    }
    private void SetShipEffectStatus(bool status) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship) {
            ship.ShipHealth.HealHPByPercentLoopStatus(status);
        }
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        ShowBoss(() => {
            UpdateUI();
            base.OnShow(onCompleted, instant);
        });
    }
    public void AddOnClose(Action onClose) {
        this.onClose = onClose;
    }
    private void OnClose() {
        HideBoss(() => {
            onClose?.Invoke();
            Hide();
        });
        SetShipEffectStatus(true);
    }
    private void SpinLayoutDone() {
        if (GameResources.Instance.AutoPlay) {
            OnClose();
            return;
        }
        if (!data.Spinable()) {
            OnClose();
            return;
        }
        if (hideAnimation != null)
            hideAnimation.Play(() => {
                SetLayoutStatus(false);
                if (showAnimation != null)
                    showAnimation.Play();
            }, true);
    }
    private void SetLayoutStatus(bool status) {
        spinLayout.gameObject.SetActive(status);
        bonusLayout.gameObject.SetActive(!status);
    }
    public override Frame OnBack() {
        if (backButton.gameObject.activeInHierarchy)
            OnClose();
        return this;
    }
}
