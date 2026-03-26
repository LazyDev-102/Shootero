using DG.Tweening;
using GameSystem.Common.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngleOfferPopup : DOTweenFrame {

    [SerializeField] private ModItemDisplayer modItem1;
    [SerializeField] private ModItemDisplayer modItem2;
    [SerializeField] private Image background;

    private AngelBoss angelBoss;

    public AngleOfferPopup SetAngelBoss(AngelBoss angelBoss) {
        this.angelBoss = angelBoss;
        return this;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        GeneralMods();
        SetupShow();
        if (GameResources.Instance.AutoPlay) {
            DOVirtual.DelayedCall(1, modItem1.ChooseCheat);
        }
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        IngameHUD.Instance.Combat.ShowModInfo.ShowNewMod();
    }
    public void GeneralMods() {
        var modData = GameResources.Instance.ModGenerator.GetRandomModDatasWithIndexs(new int[] { 2, 3, 16, 17 });
        SetDisplayer(modData);
    }
    private void SetupShow() {
        background.SetAlpha(0);
        modItem1.SetAlphaAll(0.2f);
        modItem2.SetAlphaAll(0.2f);
        background.DOFade(0.7f, 0.2f);
        DOVirtual.DelayedCall(0.5f, () => {
            GameManager.Instance.Pause();
        });
    }
    private void SetDisplayer(ModData modData2) {
        SetDataItem2(modData2);
        SetDataItem1();
    }
    private void SetDataItem1() {
        modItem1.transform.localScale = Vector3.one;
        modItem1.SetAlphaAll(0.5f, () => modItem1.CanClick(true))
        .OnItemClicked(() => {
            if (!gameObject.activeInHierarchy)
                return;
            GameManager.Instance.GameLoader.Ship.ShipHealth.AddHpByPercent(0.5f);
            modItem1.CanClick(false);
            modItem2.CanClick(false);
            modItem2.transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true);
            modItem1.transform.DOScale(Vector3.one * 1.5f, 0.25f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => {
                modItem1.transform.DOScale(Vector3.zero, 0.2f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => OnClose());
            });
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnTake4ngel);
        });
    }
    private void SetDataItem2(ModData modData2) {
        modItem2.transform.localScale = Vector3.one;
        modItem2.SetIcon(modData2.Icon).SetName(modData2.NameMod).SetAlphaAll(0.5f, () => modItem2.CanClick(true))
        .OnItemClicked(() => {
            if (!gameObject.activeInHierarchy)
                return;
            modData2.ApplyTo(GameManager.Instance.GameLoader.Ship);
            //GameManager.Instance.GameLoader.Ship.ShipLevel.CurrentUpgradeLevel++;
            modItem1.CanClick(false);
            modItem2.CanClick(false);
            modItem1.transform.DOScale(Vector3.zero, 0.3f).SetUpdate(true);
            modItem2.transform.DOScale(Vector3.one * 1.5f, 0.25f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => {
                modItem2.transform.DOScale(Vector3.zero, 0.2f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => {
                    //IngameHUD.Instance.Combat.ShowNewModInfo(modData2);
                    IngameHUD.Instance.Combat.ShowModInfo.AddModInfor(modData2);
                    OnClose();
                });
            });
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnTake4ngel);
        });
    }

    private void OnClose() {
        GameManager.Instance.Resume();
        angelBoss.EndMove(() => GameManager.Instance.NextWave());
        Hide();
    }
    public override Frame OnBack() {
        return this;
    }
}
