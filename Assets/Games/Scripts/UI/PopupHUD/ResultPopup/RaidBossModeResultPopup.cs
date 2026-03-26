using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using GameSystem.Common.UI;
using DG.Tweening;
using Gemmob;

public class RaidBossModeResultPopup : BasePopup {
    [SerializeField] private TextMeshProUGUI txtReachLevelWin;
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

    protected override void Start() {
        base.Start();
        btnSkip?.AddEvent(OnSkipButtonClicked);
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
        isSkiped = false;
        isAddExp = false;
        btnSkip.gameObject.SetActive(true);
        LevelProgressData levelProgress = GameResources.Instance.LevelProgress;
        var oldExp = levelProgress.Datas.OwnedExp;
        var oldMaxExp = levelProgress.Datas.GetMaxExpInLevel();
        oldRatio = Convert.ToSingle(oldExp) / Convert.ToSingle(oldMaxExp);
        oldLevel = levelProgress.Datas.CurrentLv;
        levelBar.ForceFillAmountBar(oldRatio);
        SetContentLevelProgress($"{oldLevel + 1}", true);
        StartCoroutine(IDelayLevelBar());
        txtTapContinue.gameObject.SetActive(false);
        menuReward.gameObject.SetActive(false);
        btnSkip.interactable = txtTapContinue.gameObject.activeInHierarchy;
    }
    private void GetReward() {
        GameResources.Instance.MaterialModeData.ClaimReward(currentWave, isWin);
    }
    private IEnumerator IDelayLevelBar() {
        yield return Yielder.Wait(delayShowLevelBar);
        LevelProgressData levelProgress = GameResources.Instance.LevelProgress;
        if (levelProgress.Datas.MaxLevel) {
            oldRatio = 1;
            newRatio = 1;
            oldLevel = 99;
            newLevel = 99;
        }
        else {
            newRatio = levelProgress.GetRatio();
            newLevel = levelProgress.GetCurrentLevel();
        }

        CheckLevelup();
    }
    private void CheckLevelup() {
        if (oldLevel != newLevel) {
            SetContentLevelProgress($"{oldLevel + 1}", true);
            SetLevelBar(oldRatio, 1, () => {
                SetContentLevelProgress($"{newLevel + 1}", true);
                oldRatio = 0;
                oldLevel++;
                PopupHUD.Instance.Show<LevelUpPopup>().SetData().AddOnClose(CheckLevelup).Show();
                if (oldLevel < newLevel)
                    GameResources.Instance.LevelProgress.Datas.PointLevelup++;
            });
        }
        else {
            SetContentLevelProgress($"{newLevel + 1}", true);
            SetLevelBar(oldRatio, newRatio, () => {
                SetMenuRewardStatus();
                ShowItemClaimedCollector();
            });
        }
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
        if (items.Count > 0) {
            ShipBase ship = GameManager.Instance.GameLoader.Ship;
            var itemChip = itemCollection.GetDisplayer(0);
            int chipIndex = -1;
            do {
                chipIndex++;
            } while (chipIndex < items.Count && items[chipIndex].Id != ConstantItemID.ChipId);
            if (chipIndex < items.Count) {
                var numberChip = $"{items[chipIndex].Amount + GameManager.Instance.GameController.GetChipGain()}";
                itemChip.ItemIcon.sprite = chipIcon;
                itemChip.SetContentAmount(numberChip, true);
            }
        }
        btnSkip.gameObject.SetActive(false);
        yield return IDelayTapEnd();
    }

    private void OnSkipButtonClicked() {
        isSkiped = true;
        btnSkip.gameObject.SetActive(false);
        showAnimation?.Stop(true);
        StopAllCoroutines();
        LevelProgressData levelProgress = GameResources.Instance.LevelProgress;
        if (!isAddExp) {
            //levelProgress.AddExp(GameResourcesIG.Instance.MaterialModeData.GetExp(currentWave));
        }
        int newExp = levelProgress.Datas.OwnedExp;
        var newMaxExp = levelProgress.Datas.GetMaxExpInLevel();
        newRatio = Convert.ToSingle(newExp) / Convert.ToSingle(newMaxExp);
        newLevel = levelProgress.Datas.CurrentLv;
        levelBar.ForceFillAmountBar(newRatio);
        SetContentLevelProgress($"{newLevel + 1}", true);
        List<ItemStack> items = GameManager.Instance.ItemClaimedCollector;
        items.Sort((a, b) => a.Id.CompareTo(b.Id));
        SetItemClaimedCollector(items, items.Count);
        if (items.Count > 0) {
            ShipBase ship = GameManager.Instance.GameLoader.Ship;
            if (ship && ship.ShipStat) {
                var itemChip = itemCollection.GetDisplayer(0);
                var numberChip = $"{items[0].Amount + GameManager.Instance.GameController.GetChipGain()}";
                itemChip.ItemIcon.sprite = chipIcon;
                itemChip.SetContentAmount(numberChip, true);
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

    public RaidBossModeResultPopup SetWave(int waveIndex) {
        txtReachLevelWin.text = $"Wave {waveIndex + 1}";
        return this;
    }

    public RaidBossModeResultPopup SetWin(bool isWin) {
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
    public void SetMaterialContent(int currentWave) {
        this.currentWave = currentWave;
        SetWave(currentWave);
        GetReward();
    }
}
