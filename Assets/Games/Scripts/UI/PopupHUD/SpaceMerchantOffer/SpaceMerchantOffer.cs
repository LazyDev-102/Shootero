using DG.Tweening;
using GameSystem.Common.UI;
using Gear_Data;
using Helper;
using Spine.Unity;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Gemmob;

public class SpaceMerchantOffer : DOTweenFrame {

    [SerializeField] private SpaceMerchantItem[] items;
    [SerializeField] private SkeletonGraphic spaceMerchant;
    [SerializeField] private ItemCollector gearCollector;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private GameObject mainFrameGO;
    [SerializeField] private ParticleSystem chargeWhite;
    [SerializeField] private ParticleSystem glowBurst;

    private SpaceMerchantData data;
    private GearSoftData[] gears;
    private Action onClose;
    private void Awake() {
        SetData(GameResources.Instance.SpaceMerchant);
        AssignEvent();
    }
    private void SetData(SpaceMerchantData data) {
        this.data = data;
    }
    private void AssignEvent() {
        gears = new GearSoftData[data.MaxItem];
        backButton.AddEvent(OnClose);
        for (int i = 0; i < items.Length; i++) {
            items[i].Assign();
        }
    }
    public void AddOnClose(Action onClose) {
        this.onClose = onClose;
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        SetShipEffectStatus(false);
        SetActive(false);
        GenGear();
        SetupShow();
        if (GameResources.Instance.AutoPlay) {
            DOVirtual.DelayedCall(1, OnClose);
        }
    }
    public void GenGear() {
        GearHardData[] gearHardData = new GearHardData[data.MaxItem];
        for (int i = 0; i < data.MaxItem; i++) {
            int loop = 0;
            GearHardData newGear = null;
            do {
                newGear = (GearHardData)RandomHelper.RandomInCollection(gearCollector.Items);
                loop++;
                if (loop > 5)
                    break;
            } while (gearHardData.Contains(newGear));
            gearHardData[i] = newGear;
            gears[i] = new GearSoftData(gearHardData[i].Id, data.GetRank());
        }
    }
    private IEnumerator SetupItem() {
        for (int i = 0; i < items.Length; i++) {
            items[i].UpdateUI(gears[i], data.GetPrice(gears[i].GearTypeSoft, gears[i].GearHardData.GetRarety(gears[i].CurrentRank).Type));
            yield return Yielder.Wait(0.2f);
        }
    }
    private void SetupShow() {
        StartCoroutine(SetupItem());
        if (gameObject.activeSelf)
            StartCoroutine(PlayAnim());
        //DOVirtual.DelayedCall(0.5f, () => SetupItem()).SetUpdate(true);
    }
    private IEnumerator PlayAnim() {
        if (spaceMerchant != null) {
            spaceMerchant.transform.localScale = Vector3.one;
            spaceMerchant.SetAlpha(1);
            if (spaceMerchant.AnimationState != null) {
                spaceMerchant.AnimationState.SetAnimation(0, "Show", false);
                yield return Yielder.Wait(1f);
                spaceMerchant.AnimationState.SetAnimation(0, "Idle", true);
                yield return Yielder.Wait(1);
                SetActive(true);
            }
        }
    }
    private void OnClose() {
        if (chargeWhite != null)
            chargeWhite.Play();
        SetShipEffectStatus(true);
        GameManager.Instance.Resume();
        if (hideAnimation != null)
            hideAnimation.Play();
        spaceMerchant.transform.DOScale(Vector3.zero, 1f);
        spaceMerchant.DOFade(0, 1f);
        DOVirtual.DelayedCall(1f, () => {
            if (chargeWhite != null)
                chargeWhite.Stop();
            DeactiveItem();
            onClose?.Invoke();
            //Hide();
            gameObject.SetActive(false);
        });
    }
    private void DeactiveItem() {
        for (int i = 0; i < items.Length; i++) {
            items[i].gameObject.SetActive(false);
        }
    }
    public override Frame OnBack() {
        return this;
    }
    private void SetActive(bool status) {
        mainFrameGO.SetActive(status);
        if (status) {
            if (glowBurst)
                glowBurst.Play();
            if (showAnimation)
                showAnimation.Play();
        }
    }
    private void SetShipEffectStatus(bool status) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship) {
            ship.ShipHealth.HealHPByPercentLoopStatus(status);
        }
    }
}
