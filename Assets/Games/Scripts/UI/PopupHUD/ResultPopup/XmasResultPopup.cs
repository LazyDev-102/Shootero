
using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using GameSystem.Common.UI;
using DG.Tweening;
using Gemmob;

public class XmasResultPopup : BasePopup {
    [SerializeField] private TextMeshProUGUI txtLevlProgress;
    [SerializeField] private TextMeshProUGUI txtCurrentPercentLevel;
    [SerializeField] private ProgressFillAmountBase levelBar;
    [SerializeField] private ItemCollectionDisplayer itemCollection;
    [SerializeField] private ButtonBase btnSkip;
    [SerializeField] private TextMeshProUGUI txtTapContinue;
    [SerializeField] private Image imgGradient;
    [SerializeField] private GameObject headerWin;
    [SerializeField] private Sprite chipIcon;
    [SerializeField] private Image menuReward;

    [Header("Anim")]
    [SerializeField] private float delayShowHeader;
    [SerializeField] private float delayShowLevelBar;
    [SerializeField] private float delayShowItems;
    [SerializeField] private float deltaShowItems;
    [SerializeField] private float delayTapEnd;

    private bool isWin;
    private bool isSkiped;
    private LevelProgressData levelData;

    protected override void Start() {
        base.Start();
        if (btnSkip != null)
            btnSkip.AddEvent(OnSkipButtonClicked);
    }
    public override Frame OnBack() {
        if (isSkiped) {
            return base.OnBack();
        }
        OnSkipButtonClicked();
        return this;
    }
    private void SetData() {
        if (levelData == null)
            levelData = GameResources.Instance.LevelProgress;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        isSkiped = false;
        SetData();
        UpdateUILevel();
        UpdateUIOnShow();
        StartCoroutine(IDelayLevelBar());
    }
    private void UpdateUIOnShow() {
        txtTapContinue.gameObject.SetActive(false);
        menuReward.gameObject.SetActive(false);
        btnSkip.gameObject.SetActive(true);
        btnSkip.interactable = txtTapContinue.gameObject.activeInHierarchy;
    }
    private void UpdateUILevel() {
        var ratio = levelData.GetRatio();
        var level = levelData.GetCurrentLevel();
        levelBar.ForceFillAmountBar(ratio);
        SetContentLevelProgress($"{level + 1}", true);
    }
    private void GetReward() {
        GameResources.Instance.Xmas.ClaimReward();
    }
    private IEnumerator IDelayLevelBar() {
        yield return Yielder.Wait(delayShowLevelBar);
        SetData();
        var ratio = levelData.GetRatio();
        SetContentLevelProgress($"{levelData.GetCurrentLevel() + 1}", true);
        SetLevelBar(ratio, ratio, () => {
            SetMenuRewardStatus();
            ShowItemClaimedCollector();
        });
    }
    private void SetMenuRewardStatus() {
        menuReward.gameObject.SetActive(true);
        menuReward.SetAlpha(0);
        menuReward.DOFade(0.1f, 0.5f).SetUpdate(true);
    }
    private void ShowItemClaimedCollector() {
        StartCoroutine(IShowItemClaimedCollector());
    }

    private IEnumerator IShowItemClaimedCollector() {
        yield return Yielder.Wait(delayShowItems);
        List<ItemStack> items = GameManager.Instance.ItemClaimedCollector;
        items.Sort((a, b) => a.Id.CompareTo(b.Id));
        for (int i = 0; i < items.Count; ++i) {
            SetItemClaimedCollector(items, i + 1);
            yield return Yielder.Wait(deltaShowItems);
        }
        SetFrameGear();
        CaculatorChipGain(items);
        btnSkip.gameObject.SetActive(false);
        yield return IDelayTapEnd();
    }

    private void CaculatorChipGain(List<ItemStack> items) {
        if (items.Count > 0) {
            var itemChip = itemCollection.GetDisplayer(0);
            int chipIndex = -1;
            do {
                chipIndex++;
            } while (chipIndex < items.Count && items[chipIndex].Id != ConstantItemID.ChipId);
            if (chipIndex < items.Count) {
                var numberChip = items[chipIndex].Amount + GameManager.Instance.GameController.GetChipGain();
                itemChip.ItemIcon.sprite = chipIcon;
                itemChip.SetContentAmount($"{numberChip}", true);
                Debug.Log("111111");
            }
        }
    }

    private void OnSkipButtonClicked() {
        isSkiped = true;
        btnSkip.gameObject.SetActive(false);
        if (showAnimation != null)
            showAnimation.Stop(true);
        StopAllCoroutines();
        SetData();
        UpdateUILevel();
        ShowRewardUI();
        StartCoroutine(IDelayTapEnd());
    }
    private void ShowRewardUI() {
        List<ItemStack> items = GameManager.Instance.ItemClaimedCollector;
        items.Sort((a, b) => a.Id.CompareTo(b.Id));
        SetItemClaimedCollector(items, items.Count);
        if (items.Count > 0) {
            ShipBase ship = GameManager.Instance.GameLoader.Ship;
            if (ship && ship.ShipStat) {
                var itemChip = itemCollection.GetDisplayer(0);
                var numberChip = items[0].Amount + GameManager.Instance.GameController.GetChipGain();
                itemChip.ItemIcon.sprite = chipIcon;
                itemChip.SetContentAmount($"{numberChip}", true);
                Tracking.Instance.LogCurrency(1, numberChip, ScreenName);
            }
        }
    }

    private IEnumerator IDelayTapEnd() {
        HUDManager.IgnoreUserInput(true);
        yield return Yielder.Wait(delayTapEnd);
        HUDManager.IgnoreUserInput(false);
        txtTapContinue.gameObject.SetActive(true);
        btnSkip.interactable = txtTapContinue.gameObject.activeInHierarchy;
    }

    private void SetItemClaimedCollector(List<ItemStack> items, int number) {
        if (itemCollection) {
            itemCollection.SetCapacity(number).SetItems(items).Show();
        }
    }
    private void SetFrameGear() {
        var data = GameResources.Instance.Xmas;
        var gIds = data.GearIds;
        var gRanks = data.GearRanks;
        foreach (var item in itemCollection.GetAllItem()) {
            for (int i = 0; i < gIds.Count; i++) {
                if (item.Id == gIds[i])
                    if (itemCollection.GetItemView(item) is ClaimedItemView civ) {
                        civ.SetBorder(gIds[i], gRanks[i]);
                    }
            }
        }
    }
    private void SetLevelBar(float oldRatio, float newRatio, Action onComplete = null, bool show = true) {
        if (levelBar) {
            levelBar.gameObject.SetActive(show);
            if (show) {
                levelBar.AddOnComplete(onComplete);
                levelBar.FillBar(oldRatio, newRatio);
            }
        }
    }

    private void SetContentLevelProgress(string content, bool show) {
        if (txtLevlProgress) {
            txtLevlProgress.gameObject.SetActive(show);
            if (show) {
                txtLevlProgress.text = content;
            }
        }
    }

    public XmasResultPopup SetWin(bool isWin) {
        this.isWin = isWin;
        StartCoroutine(PlayEffectHeader());
        return this;
    }
    private IEnumerator PlayEffectHeader() {
        yield return Yielder.Wait(delayShowHeader);
        headerWin.SetActive(true);
        headerWin.transform.localScale = Vector3.one * 0.5f;
        headerWin.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    public void SetGearContent() {
        if (isWin)
            GetReward();
    }
    //#if UNITY_EDITOR
    //    [SerializeField] Halloweenres reference;

    //    [ContextMenu("Convert")]
    //    private void Convert() {
    //        txtLevlProgress = reference.TxtLevlProgress;
    //        txtCurrentPercentLevel = reference.TxtCurrentPercentLevel;
    //        levelBar = reference.LevelBar;
    //        itemCollection = reference.ItemCollection;
    //        btnSkip = reference.BtnSkip;
    //        txtTapContinue = reference.TxtTapContinue;
    //        imgGradient = reference.ImgGradient;
    //        headerWin = reference.HeaderWin;
    //        chipIcon = reference.ChipIcon;
    //        menuReward = reference.MenuReward;
    //        delayShowHeader = reference.DelayShowHeader;
    //        delayShowLevelBar = reference.DelayShowLevelBar;
    //        delayShowItems = reference.DelayShowItems;
    //        deltaShowItems = reference.DeltaShowItems;
    //        delayTapEnd = reference.DelayTapEnd;
    //    }
    //#endif
}
