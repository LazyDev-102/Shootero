using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using GameSystem.Common.UI;
using DG.Tweening;
using Gemmob;

public class ResultPopup : BasePopup {
    [SerializeField] private TextMeshProUGUI txtReachLevelLose;
    [SerializeField] private TextMeshProUGUI txtReachLevelWin;
    [SerializeField] private TextMeshProUGUI txtLevlProgress;
    [SerializeField] private TextMeshProUGUI txtCurrentPercentLevel;
    [SerializeField] private ProgressFillAmountBase levelBar;
    [SerializeField] private ItemCollectionDisplayer itemCollection;
    [SerializeField] private ButtonBase btnSkip;
    [SerializeField] private TextMeshProUGUI txtTapContinue;
    [SerializeField] private Image imgGradient;
    [SerializeField] private GameObject headerWin;
    [SerializeField] private GameObject headerLose;
    [SerializeField] private Sprite chipIcon;
    [SerializeField] private Image menuReward;

    [Header("Anim")]
    [SerializeField] private float delayShowHeader;
    [SerializeField] private float delayShowLevelBar;
    [SerializeField] private float delayShowItems;
    [SerializeField] private float deltaShowItems;
    [SerializeField] private float delayTapEnd;

    [Header("Infinity")]
    [SerializeField] private GameObject infinityGroup;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScroreText;

    private bool isAddExp;
    private bool isWin;
    private bool isSkiped;
    private int oldLevel;
    private int newLevel;
    private float oldRatio;
    private float newRatio;
    private LevelProgressData levelData;

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
    private void SetData() {
        if (levelData == null)
            levelData = GameResources.Instance.LevelProgress;
    }
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        SetData();
        UpdateUIOldLevel();
        UpdateUIOnShow();
        StartCoroutine(IDelayLevelBar());
    }
    private void UpdateUIOnShow() {
        isSkiped = false;
        isAddExp = false;
        btnSkip.gameObject.SetActive(true);
        txtTapContinue.gameObject.SetActive(false);
        menuReward.gameObject.SetActive(false);
        btnSkip.interactable = txtTapContinue.gameObject.activeInHierarchy;
    }
    private void UpdateUIOldLevel() {
        oldRatio = levelData.GetRatio();
        oldLevel = levelData.GetCurrentLevel();
        levelBar.ForceFillAmountBar(oldRatio);
        SetContentLevelProgress($"{oldLevel + 1}", true);
    }
    private IEnumerator IDelayLevelBar() {
        SetData();
        if (levelData.Datas.MaxLevel) {
            oldRatio = 1;
            newRatio = 1;
            oldLevel = 99;
            newLevel = 99;
            CheckLevelup();
            yield break;
        }
        yield return Yielder.Wait(delayShowLevelBar);
        oldRatio = levelData.GetRatio();
        oldLevel = levelData.GetCurrentLevel();
        AddExp(!isAddExp);
        isAddExp = true;
        newRatio = levelData.GetRatio();
        newLevel = levelData.GetCurrentLevel();
        CheckLevelup();
    }
    private void AddExp(bool status) {
        if (status && !GameManager.Instance.IsTrial) {
            if (IngameData.currentGameMode == GameMode.Infinity) {
                var inf = GameManager.Instance.GetGameController<InfinityController>();
                if (inf != null)
                    levelData.AddExp((float)((float)(inf.CurrentWaveIndex + 1) / 8f));
            }
            else
                levelData.AddExp(GameResources.Instance.ConquerorData.CurrentZone.GetRate(this.isWin));
        }
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
                    levelData.Datas.PointLevelup++;
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
            var numberChip = $"{items[0].Amount + GameManager.Instance.GameController.GetChipGain()}";
            itemChip.ItemIcon.sprite = chipIcon;
            itemChip.SetContentAmount(numberChip, true);
        }
        btnSkip.gameObject.SetActive(false);
        yield return IDelayTapEnd();
    }
    private void OnSkipButtonClicked() {
        if (showAnimation != null)
            showAnimation.Stop(true);
        isSkiped = true;
        btnSkip.gameObject.SetActive(false);
        StopAllCoroutines();
        SetData();
        AddExp(!isAddExp);
        ShowUINewLevel();
        ShowRewardUI();
        StartCoroutine(IDelayTapEnd());
    }
    private void ShowUINewLevel() {
        newRatio = levelData.GetRatio();
        newLevel = levelData.GetCurrentLevel();
        levelBar.ForceFillAmountBar(newRatio);
        SetContentLevelProgress($"{newLevel + 1}", true);
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
                Tracking.Instance.LogCurrency(1, numberChip, IngameData.currentGameMode == GameMode.Conqueror ? ScreenName : "ingame_infinity");
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
    public ResultPopup SetWave(int zoneIndex, int waveIndex) {
        if (GameManager.Instance.IsTrial) {
            txtReachLevelLose.text = $"Trial";
            txtReachLevelWin.text = $"Trial";
        }
        else {
            txtReachLevelLose.text = $"Zone {zoneIndex + 1} - {waveIndex + 1}";
            txtReachLevelWin.text = $"Zone {zoneIndex + 1} - {waveIndex + 1}";
        }
        return this;
    }
    public ResultPopup InfinitySetContent() {
        infinityGroup.SetActive(true);
        headerLose.SetActive(false);
        headerWin.SetActive(false);
        var highScore = GameResources.Instance.UserProfile.GetHighScore();
        int currentScore = highScore;
        var controller = GameManager.Instance.GetGameController<InfinityController>();
        highScroreText.text = $"{highScore}";
        if (controller != null && controller.CurrentScore * 100 < highScore) {
            currentScore = controller.CurrentScore * 50;
        }
        currentScoreText.text = $"Score {currentScore}";
        return this;
    }
    public ResultPopup SetWin(bool isWin) {
        this.isWin = isWin;
        StartCoroutine(PlayEffectHeader(isWin));
        return this;
    }
    private IEnumerator PlayEffectHeader(bool isWin) {
        yield return Yielder.Wait(delayShowHeader);
        bool infinityMode = IngameData.currentGameMode == GameMode.Infinity;
        headerWin.SetActive(isWin && !infinityMode);
        headerLose.SetActive(!isWin && !infinityMode);
        infinityGroup.SetActive(infinityMode);
        if (isWin) {
            headerWin.transform.localScale = Vector3.one * 0.5f;
            headerWin.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        }
        else {
            headerLose.transform.localScale = Vector3.one * 0.5f;
            headerLose.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
        }
    }
}
