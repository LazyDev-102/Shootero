using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using GameSystem.Common.UI;
using DG.Tweening;
using Gemmob;

public class BossModeResultPopup : BasePopup {
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

    private bool isAddExp;
    private bool isWin;
    private bool isSkiped;
    private int oldLevel;
    private int newLevel;
    private float oldRatio;
    private float newRatio;
    private int currentWave;
    private LevelProgressData levelData;

    protected override void Start() {
        base.Start();
        if (btnSkip != null)
            btnSkip.AddEvent(OnSkipButtonClicked);
    }
    private void SetData() {
        if (levelData == null)
            levelData = GameResources.Instance.LevelProgress;
    }
    public override Frame OnBack() {
        if (isSkiped) {
            return base.OnBack();
        }
        OnSkipButtonClicked();
        return this;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        SetData();
        isSkiped = false;
        isAddExp = false;
        btnSkip.gameObject.SetActive(true);
        newRatio = levelData.GetRatio();
        newLevel = levelData.GetCurrentLevel();
        oldRatio = newRatio;
        oldLevel = newLevel;
        levelBar.ForceFillAmountBar(newRatio);
        SetContentLevelProgress($"{newLevel + 1}", true);
        StartCoroutine(IDelayLevelBar());
        txtTapContinue.gameObject.SetActive(false);
        menuReward.gameObject.SetActive(false);
        btnSkip.interactable = txtTapContinue.gameObject.activeInHierarchy;
    }
    private void GetReward() {
        GameResources.Instance.BossModeData.ClaimReward();
    }
    private IEnumerator IDelayLevelBar() {
        yield return Yielder.Wait(delayShowLevelBar);
        SetData();
        newRatio = levelData.GetRatio();
        newLevel = levelData.GetCurrentLevel();
        oldRatio = newRatio;
        oldLevel = newLevel;
        CheckLevelup();
    }
    private void CheckLevelup() {
        if (GameResources.Instance.LevelProgress.Datas.MaxLevel) {
            SetMenuRewardStatus();
            ShowItemClaimedCollector();
            return;
        }
        SetContentLevelProgress($"{newLevel + 1}", true);
        SetLevelBar(oldRatio, newRatio, () => {
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
        if (items.Count > 0) {
            ShipBase ship = GameManager.Instance.GameLoader.Ship;
            var itemChip = itemCollection.GetDisplayer(0);
            int chipIndex = -1;
            do {
                chipIndex++;
            } while (chipIndex < items.Count && items[chipIndex].Id != ConstantItemID.ChipId);
            if (chipIndex < items.Count) {
                var numberChip = items[chipIndex].Amount + GameManager.Instance.GameController.GetChipGain();
                itemChip.ItemIcon.sprite = chipIcon;
                itemChip.SetContentAmount($"{numberChip}", true);
                Debug.Log("11111111");
            }
        }
        btnSkip.gameObject.SetActive(false);
        yield return IDelayTapEnd();
    }

    private void OnSkipButtonClicked() {
        isSkiped = true;
        SetData();
        btnSkip.gameObject.SetActive(false);
        if (showAnimation != null)
            showAnimation.Stop(true);
        StopAllCoroutines();
        newRatio = levelData.GetRatio();
        newLevel = levelData.GetCurrentLevel();
        oldRatio = newRatio;
        oldLevel = newLevel;
        levelBar.ForceFillAmountBar(newRatio);
        SetContentLevelProgress($"{newLevel + 1}", true);
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
        StartCoroutine(IDelayTapEnd());
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
        var data = GameResources.Instance.BossModeData;
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
                //levelBar.AddOnComplete(onComplete);
                levelBar.ForceFillAmountBar(newRatio);
                onComplete?.Invoke();
                //levelBar.FillBar(oldRatio, newRatio);
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

    public BossModeResultPopup SetWin(bool isWin) {
        this.isWin = isWin;
        StartCoroutine(PlayEffectHeader());
        GameResources.Instance.BossModeData.ChangeTurnRemain(isWin ? -1 : 0);
        return this;
    }
    private IEnumerator PlayEffectHeader() {
        yield return Yielder.Wait(delayShowHeader);
        headerWin.SetActive(true);
        headerWin.transform.localScale = Vector3.one * 0.5f;
        headerWin.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    public void SetContent() {
        if (isWin)
            GetReward();
    }
}
