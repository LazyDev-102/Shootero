using GameSystem.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using DG.Tweening;

public class MysteryStationPopup : DOTweenFrame {
    [SerializeField] private Image modIcon;
    [SerializeField] private TextMeshProUGUI modName;
    [SerializeField] private TextMeshProUGUI hpTrade;
    [SerializeField] private ButtonExplorer acceptButton;
    [SerializeField] private ButtonExplorer rejectButton;
    [SerializeField] private GameObject mainFrame;

    [Header("Boss")]
    [SerializeField] private Transform startPosWingLeft;
    [SerializeField] private Transform startPosWingRight;
    [SerializeField] private Transform endPosWingLeft;
    [SerializeField] private Transform endPosWingRight;
    [SerializeField] private Transform wingLeft;
    [SerializeField] private Transform wingRight;
    [SerializeField] private Image station;
    [SerializeField] private ParticleSystem spreadEffect;
    [SerializeField] private ParticleSystem appearEffect;
    [SerializeField] private AnimationCurve wingMoveCurve;
    [SerializeField] private AnimationCurve wingMoveOut;
    [SerializeField] private GameObject circle;

    private MysteryStationData data;
    private ModData modData;
    private Action onClose;
    private ShipBase ship;
    private void Awake() {
        acceptButton.AddEvent(OnAccept);
        rejectButton.AddEvent(OnReject);
    }
    private void OnAccept() {
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        modData.ApplyTo(ship);
        data.Trade(ship);
        OnClose();
    }
    private void OnReject() {
        OnClose();
    }
    private void OnClose() {
        hideAnimation?.Play();
        MoveOut(() => {
            onClose?.Invoke();
            gameObject.SetActive(false);
        });
    }
    private void UpdateUI() {
        data = GameResources.Instance.MysteryStation;
        ship = GameManager.Instance.GameLoader.Ship;
        modData = data.GetMod();
        modName.text = modData.NameMod;
        modIcon.sprite = modData.Icon;
        hpTrade.text = $"Lose <color=red>{data.GetHpTrade(GameManager.Instance.GameLoader.Ship)} Max HP</color> to acquire";
    }
    private void SetStatus(bool status) {
        mainFrame.SetActive(status);
        if (status) {
            if (showAnimation != null)
                showAnimation.Play();
            DroneManager.Instance.SetParent(GameManager.Instance.GameLoader.transform);
        }

        if (GameResources.Instance.AutoPlay) {
            DOVirtual.DelayedCall(1, OnReject);
        }
    }
    public void AddOnClose(Action onClose) {
        this.onClose = onClose;
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        SetShipEffectStatus(false);
        UpdateUI();
        SetStatus(false);
        BossAction(() => SetStatus(true));
    }
    public override Frame OnBack() {
        return this;
    }
    private void BossAction(Action onComplete) {
        PreSettup();
        MoveIn(onComplete);
    }
    private void PreSettup() {
        if (spreadEffect && spreadEffect.isPlaying)
            spreadEffect.Stop();
        wingLeft.localPosition = startPosWingLeft.localPosition;
        wingRight.localPosition = startPosWingRight.localPosition;
        station.SetAlpha(0);
        circle.SetActive(false);
        DroneManager.Instance.SetParent(GameManager.Instance.GameLoader.Ship.transform);
    }
    private void MoveOut(Action onComplete) {
        ship.transform.position = station.transform.position;
        wingLeft.DOLocalMove(startPosWingLeft.localPosition, 2f).SetEase(wingMoveOut);
        wingRight.DOLocalMove(startPosWingRight.localPosition, 2f).SetEase(wingMoveOut)
                .OnComplete(() => {
                    circle.SetActive(false);
                    if (spreadEffect)
                        spreadEffect.Stop();
                    station.DOFade(0, 1f).OnComplete(() => {
                        ship.ShipEffect.RemoveCanvas();
                        ship.transform.DOMove(Vector2.zero, 1f).OnComplete(() => {
                            onComplete?.Invoke();
                            ship.ShipHealth.PlayerHPBar.SetActive(true);
                        });
                    });
                });
    }
    private void MoveIn(Action onComplete) {
        wingLeft.DOLocalMove(endPosWingLeft.localPosition, 2f).SetEase(wingMoveCurve);
        wingRight.DOLocalMove(endPosWingRight.localPosition, 2f).SetEase(wingMoveCurve)
                .OnComplete(() => {
                    if (appearEffect)
                        appearEffect.Play();
                    station.DOFade(1, 1f).OnComplete(() => {
                        if (spreadEffect)
                            spreadEffect.Play();
                        circle.SetActive(true);
                        SetShipEffectStatus(true);
                        MoveShipToStation(onComplete);
                    });
                });
    }
    private void MoveShipToStation(Action onComplete) {
        ship.ShipHealth.PlayerHPBar.SetActive(false);
        ship.ShipEffect.AddCanvas();
        ship.transform.DOMove(station.transform.position, 2f).OnComplete(() => onComplete?.Invoke());
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        hideAnimation?.Play();
        MoveOut(() => {
            gameObject.SetActive(false);
        });
    }
    private void SetShipEffectStatus(bool status) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship) {
            ship.ShipHealth.HealHPByPercentLoopStatus(status);
        }
    }
}
