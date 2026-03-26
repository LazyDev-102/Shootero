using DG.Tweening;
using GameSystem.Common.UI;
using System;
using UnityEngine;

public class FullHealPopup : DOTweenFrame {
    [SerializeField] private ButtonExplorer watchButton;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private Transform healer;
    [SerializeField] private GameObject mainFrameGO;

    [Header("Boss")]
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private ParticleSystem glowBurst1;
    [SerializeField] private ParticleSystem burstEffect;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform midPos;
    [SerializeField] private Transform endPos;

    private Action onClose;
    private FullHealData data;
    private void Awake() {
        watchButton.AddEvent(OnWatch);
        backButton.AddEvent(OnClose);
    }
    private void OnEnable() {
        healer.localPosition = startPos.localPosition;
    }
    public void AddOnClose(Action onClose) {
        this.onClose = onClose;
    }
    private void OnWatch() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.full_heal, () => {
            data.Excute(GameManager.Instance.GameLoader.Ship);
            OnClose();
        });
    }
    private void OnClose() {
        SetStatus(false);
        SetShipEffectStatus(true);
        EndMove(() => {
            Hide();
            onClose?.Invoke();
        });
    }
    private void SetShipEffectStatus(bool status) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship) {
            ship.ShipHealth.HealHPByPercentLoopStatus(status);
        }
    }
    private void StartMove() {
        healer.localPosition = startPos.localPosition;
        healer.gameObject.SetActive(true);
        healer.DOLocalMove(midPos.localPosition, 1f).SetEase(moveCurve).OnComplete(() => {
            healer.DOLocalMove((Vector2)midPos.localPosition - Vector2.up * 20, 1f)
                  .SetEase(Ease.Linear)
                  .SetUpdate(true)
                  .SetLoops(-1, LoopType.Yoyo);
            DOVirtual.DelayedCall(1f, () => {
                if (glowBurst1 != null)
                    glowBurst1.Play();
                SetStatus(true);
            });
        });
    }
    public void EndMove(Action onComplete) {
        healer.DOLocalMove((Vector2)midPos.localPosition + Vector2.up, 0.5f).SetEase(Ease.Linear).OnComplete(() => {
            if (glowBurst1 != null)
                burstEffect.Play();
            healer.DOLocalMove(endPos.localPosition, 1f).OnComplete(() => {
                onComplete?.Invoke();
                healer.gameObject.SetActive(false);
            });
        });
    }
    private void SetStatus(bool status) {
        mainFrameGO.SetActive(status);
        if (status && showAnimation != null) {
            showAnimation.Play();
        }
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        data = GameResources.Instance.FullHeal;
        if (GameResources.Instance.AutoPlay) {
            data.Excute(GameManager.Instance.GameLoader.Ship);
            OnClose();
        }
        else {
            SetShipEffectStatus(false);
            SetStatus(false);
            StartMove();
        }
    }

    public override Frame OnBack() {
        return this;
    }
}
