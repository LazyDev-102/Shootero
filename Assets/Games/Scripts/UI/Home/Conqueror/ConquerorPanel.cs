using DG.Tweening;
using GameSystem.Common.UI;
using Gemmob;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gemmob.Tutorial;
using System.Collections;

public class ConquerorPanel : DOTweenFrame {
    #region Variables
    [SerializeField] private ButtonExplorer settingButton;
    [SerializeField] private ButtonExplorer progressRewardButton;
    [SerializeField] private ButtonExplorer playGameButton;
    [SerializeField] private ButtonExplorer playInfinityButton;
    [SerializeField] private ButtonExplorer previousZoneButton;
    [SerializeField] private ButtonExplorer nextZoneButton;
    [SerializeField] private ButtonExplorer afkButton;
    [SerializeField] private ButtonExplorer dailyLoginButton;
    [SerializeField] private ButtonExplorer RookieLoginButton;
    [SerializeField] private ButtonExplorer dailyPacksButton;
    [SerializeField] private ButtonExplorer missionButton;
    [SerializeField] private ButtonExplorer smartOfferButton;
    [SerializeField] private ButtonExplorer shipPackButton;
    [SerializeField] private NotiListener rookieListener;
    [SerializeField] private NotiListener dailyListener;
    [SerializeField] private NotiListener dailyPacksListener;
    [SerializeField] private NotiListener missionListener;
    [SerializeField] private NotiListener afkListener;
    [SerializeField] private NotiListener zoneProgressListener;
    [SerializeField] private TextMeshProUGUI zoneName;
    [SerializeField] private TextMeshProUGUI highestWave;
    [SerializeField] private TextMeshProUGUI reachZoneText;
    [SerializeField] private Image lockNextZoneImage;
    [SerializeField] private Image nextZoneImage;
    [SerializeField] private Image bossIcon;
    [SerializeField] private Image smartOfferIcon;
    [SerializeField] private Image frameIcon;
    [SerializeField] private BossUI bossUI;
    [SerializeField] private ItemView energyPrice;
    [SerializeField] private GameObject newNextZoneGO;
    [SerializeField] private CanvasGroup mainFrame;
    [SerializeField] private ButtonBase battlePassButton;
    [SerializeField] private SpreadEffectUI spreadEffect;
    [SerializeField] private BattlePassSlider passSlider;
    [SerializeField] private LockbarNotify lockbar;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showLeftToRight;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showRightToLeft;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideLeftToRight;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideRightToLeft;

    [Header("Offline")]
    [SerializeField] private GameObject offlineBoard;
    [SerializeField] private GameObject offlineSettingText;
    [SerializeField] private ButtonBase offlineBackgroundButton;
    [SerializeField] private ButtonBase offlineConfirmButton;
    [SerializeField] private bool offlineShowed;

    [Header("Cheat")]
    [SerializeField] private GameObject Cheat;


    private int cZoneSelect;
    private int unlockZone;
    private TutorialSytemData tutData;
    #endregion

    #region Init Function
    private void Awake() {
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.Login, 1);
        EventDispatcher.Instance.Dispatch(EventKey.OnLogin);
        settingButton.AddEvent(OpenSettingPopup);
        progressRewardButton.AddEvent(OpenProgressReward);
        playGameButton.AddEvent(PlayGame);
        previousZoneButton.AddEvent(PriviousZoneClick);
        nextZoneButton.AddEvent(NextZoneClick);
        dailyLoginButton.AddEvent(OpenDailyLoginPopup);
        RookieLoginButton.AddEvent(OpenRookieLoginPopup);
        dailyPacksButton.AddEvent(OpenDailyPacksPopup);
        afkButton.AddEvent(OpenAfkPopup);
        missionButton.AddEvent(OpenMissionPopup);
        playInfinityButton.AddEvent(OpenInfinityPanel);
        smartOfferButton.AddEvent(OpenSmartOffer);
        battlePassButton.AddEvent(OpenBattlePass);
        shipPackButton.AddEvent(OpenShipPackPopup);
        offlineBackgroundButton.AddEvent(CloseOfflineBoard);
        offlineConfirmButton.AddEvent(CloseOfflineBoard);
        cZoneSelect = 1;
        EventDispatcher.Instance.AddListener(EventKey.OnEnergyChanged, OnEnergyChanged);
        EventDispatcher.Instance.AddListener(EventKey.OnGearInventoryChange, SmartOfferStatus);
        cZoneSelect = GameResources.Instance.ConquerorData.CurrentZoneIndex + 1;
        Cheat.SetActive(false);
#if CHEAT
        Cheat.SetActive(true);
#endif
    }

    protected void OnDestroy() {
        cZoneSelect = 1;
        EventDispatcher.Instance.RemoveListener(EventKey.OnEnergyChanged, OnEnergyChanged);
        EventDispatcher.Instance.RemoveListener(EventKey.OnGearInventoryChange, SmartOfferStatus);
    }

    public override Frame SetAnimShow(bool leftToRight) {
        showAnimation = leftToRight ? showLeftToRight : showRightToLeft;
        return this;
    }
    public override Frame SetAnimHide(bool leftToRight) {
        hideAnimation = leftToRight ? hideLeftToRight : hideRightToLeft;
        return this;
    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        if (cZoneSelect > GameResources.Instance.ConquerorData.UnlockZone + 1)
            cZoneSelect = 1;
        UpdateUI();
        ToolbarScaler.Instance.SetActive(true);
        HeadHUD.Instance.Show<HeadPanel>();
        mainFrame.alpha = 1;
        ShowTutorial();
        ShowOfflineBoard();
    }
    #region Tutorial
    private void SetTutData() {
        if (tutData == null)
            tutData = GameResources.Instance.TutorialSytemData;
    }
    private void ShowTutorial() {
        SetTutData();
        ShowTutorialOpenChest();
        ShowTutorialEquipment();
        ShowOpenSkillsTutorial();
        ShowEquipSkillsTutorial();
        ShowTutorialPlayInfinity();
    }
    private void ShowTutorialOpenChest() {
        if (!tutData.FinishTutorialOpenChest) {
            if (GameResources.Instance.Inventory.GetItem(ConstantItemID.NormalKey).Amount == 0)
                GameResources.Instance.Inventory.Add(ConstantItemID.NormalKey, 1);
            TutorialSystem.Instance.SetTimeActiveCanvas(0.5f)
                                    .InitPointer(Vector3.one, 1f, "", 5)
                                    .SetCamera()
                                    .GetData(TutorialKey.TutorialOpenChest)
                                    .AssignTarget(TutorialKey.TutorialOpenChest, 0, ToolbarScaler.Instance.GetTabObject(ToolBarType.Shop))
                                    .ShowTutorial(OnCompleteTutorialOpenChest);
        }
    }
    private void ShowTutorialEquipment() {
        if (tutData.FinishAllTutorial)
            return;
        if (CanShowTutorialEquipment()) {
            TutorialSystem.Instance.SetTimeActiveCanvas(1.5f)
                                    .GetData(TutorialKey.TutorialEquipment)
                                    .SetBackgroundButtonAlpha(0)
                                    .AssignTarget(TutorialKey.TutorialEquipment, 0, ToolbarScaler.Instance.GetTabObject(ToolBarType.Gears))
                                    .AssignTarget(TutorialKey.TutorialEquipment, 5, playGameButton.gameObject)
                                    .ShowTutorial(OnCompleteTutorialEquipment);
        }
        else {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.5f)
                                    .SetBackgroundButtonAlpha(0)
                                    .AssignTarget(TutorialKey.TutorialEquipment, 5, playGameButton.gameObject);
        }
    }
    private void ShowTutorialPlayInfinity() {
        if (tutData.CanShowTutorialPlayInfinity()) {
            SpecialTriggerSystem.Instance.AddOnEnd(() => {
                var reward = PopupHUD.Instance.GetActiveFrame<RewardPopup>();
                if (reward != null) {
                    reward.AddOnClose(ForceShowInfinityTutorial);
                }
                else
                    ForceShowInfinityTutorial();
            });
        }
    }
    private void ForceShowInfinityTutorial() {
        if (playInfinityButton != null) {
            TutorialSystem.Instance.SetTimeActiveCanvas(1.5f)
                                            .GetData(TutorialKey.TutorialPlayInfinity)
                                            .AssignTarget(TutorialKey.TutorialPlayInfinity, 0, playInfinityButton.gameObject)
                                            .ShowTutorial(OnCompleteTutorialInfinity);
        }
    }
    private void ShowOpenSkillsTutorial() {
        if (tutData.CanShowOpenSkillTutorial()) {
            SpecialTriggerSystem.Instance.AddOnEnd(() => {
                var reward = PopupHUD.Instance.GetActiveFrame<RewardPopup>();
                if (reward != null) {
                    reward.AddOnClose(ForceShowOpenSkillsTutorial);
                }
                else
                    ForceShowOpenSkillsTutorial();
            });
        }
    }
    private void ForceShowOpenSkillsTutorial() {
        tutData.IsOpenSkillTutorial = true;
        TutorialSystem.Instance.SetTimeActiveCanvas(1.5f)
                               .GetData(TutorialKey.TutorialOpenSkill)
                               .AssignTarget(TutorialKey.TutorialOpenSkill, 0, ToolbarScaler.Instance.GetTabObject(ToolBarType.Shop))
                               .SetBackgroundButtonAlpha(0)
                               .ShowTutorial(OnCompleteTutorialOpenSkill);
    }
    private void ShowEquipSkillsTutorial() {
        if (!tutData.IsOpenSkillTutorial && tutData.CanShowEquipSkillsTutorial()) {
            SpecialTriggerSystem.Instance.AddOnEnd(() => {
                var reward = PopupHUD.Instance.GetActiveFrame<RewardPopup>();
                if (reward != null) {
                    reward.AddOnClose(ForceShowEquipSkillsTutorial);
                }
                else
                    ForceShowEquipSkillsTutorial();
            });
        }
    }
    private void ForceShowEquipSkillsTutorial() {
        TutorialSystem.Instance.SetTimeActiveCanvas(.1f)
                               .GetData(TutorialKey.TutorialEquipSkills)
                               .SetBackgroundButtonAlpha(0)
                               .InitPointer(Vector3.one, 1f, "", 5)
                               .AssignTarget(TutorialKey.TutorialEquipSkills, 0, ToolbarScaler.Instance.GetTabObject(ToolBarType.Gears))
                               .ShowTutorial(OnCompleteEquipSkillTut);
    }
    private void OnCompleteEquipSkillTut() {
        tutData.SetFinishTutorialEquipSkills(true);
    }
    private void OnCompleteTutorialOpenChest() {
        tutData.SetFinishTutorialOpenChest(true);
    }
    private void OnCompleteTutorialEquipment() {
        tutData.SetFinishTutorialEquipment(true);
    }
    private void OnCompleteTutorialInfinity() {
        tutData.SetFinishTutorialPlayInfinity(true);
    }
    private void OnCompleteTutorialOpenSkill() {
        tutData.SetFinishTutorialOpenSkill(true)
               .SetGaveSkill(true);
    }
    private bool CanShowTutorialEquipment() {
        return tutData.FinishTutorialOpenChest &&
            GameResources.Instance.GearInventory.GearItems.Count > 0 && !tutData.FinishTutorialEquipment;
    }
    #endregion

    #endregion

    #region Function 
    public void UpdateBattlePassSlider() {
        passSlider.UpdateUI();
    }
    public void DailyLoginNotify() {
        dailyListener.CheckToShow();
    }
    public void DailyLoginStatus() {
        dailyLoginButton.gameObject.SetActive(GameResources.Instance.DailyLoginData.CanUnlock() && !GameResources.Instance.DailyLoginData.IsCompleted);
    }
    public void RookieLoginStatus() {
        RookieLoginButton.gameObject.SetActive(!GameResources.Instance.RookieLoginData.IsComplete);
    }
    public void SmartOfferStatus() {
        bool active = GameResources.Instance.IapPack.SmartOffer.Active();
        smartOfferButton.gameObject.SetActive(active);
        if (active) {
            var offer = GameResources.Instance.IapPack.SmartOffer.GetOfferData();
            smartOfferIcon.sprite = offer.Reward.Icon;
            frameIcon.sprite = offer.FrameIcon;
        }
    }
    private void RateUsStatus() {
        GameResources.Instance.RateUs.ReloadDataByRemote();
    }
    private IEnumerator ShipPackStatus() {
        var shipPack = GameResources.Instance.ShipPackData;
        if (!shipPack.FirstCondition()) {
            shipPackButton.gameObject.SetActive(false);
            yield break;
        }
        shipPack.ReloadDataByRemote();
        yield return new WaitUntil(() => shipPack.LoadRemoteDone == true);
        shipPackButton.gameObject.SetActive(shipPack.Status());
    }
    public void RookieLoginNotify() {
        rookieListener.CheckToShow();
    }
    public void MissionPopupNotify() {
        missionListener.CheckToShow();
    }
    public void ZoneProgressNotify() {
        zoneProgressListener.CheckToShow();
    }
    public void AfkPopupNotify() {
        afkListener.CheckToShow();
    }
    public void DailyPacksNotify() {
        dailyPacksListener.CheckToShow();
    }
    private void UpdateUI() {
        var data = GameResources.Instance.ConquerorData.ZoneDatas[cZoneSelect - 1];
        lockbar.gameObject.SetActive(false);
        ShowEnergyPlay();
        UpdateZoneDetail(data);
        SetStatePlayGame();
        UpdateZoneRewardNotice();
        BackgroundManager.Instance.SetBackground(data.Background);
        DailyLoginStatus();
        RookieLoginStatus();
        SmartOfferStatus();
        RateUsStatus();
        StartCoroutine(ShipPackStatus());
    }
    private void UpdateZoneDetail(ConquerorZoneData data) {
        //maxZone = 4;
        unlockZone = GameResources.Instance.ConquerorData.UnlockZone + 1;
        var canNextZone = cZoneSelect < unlockZone;
        bossIcon.sprite = data.Icon;
        zoneName.text = $"Zone {cZoneSelect}: {data.NameZone}";
        highestWave.text = data.HighestWave == data.MaxWave ? "CLEAR!" : $"Highest Wave: {data.HighestWave}/{data.MaxWave}";
        previousZoneButton.gameObject.SetActive(cZoneSelect > 1);
        nextZoneButton.interactable = canNextZone;
        nextZoneImage.gameObject.SetActive(canNextZone);
        lockNextZoneImage.gameObject.SetActive(!canNextZone);
        bossUI.UpdateUI(cZoneSelect - 1);
        if (canNextZone)
            newNextZoneGO.SetActive(GameResources.Instance.ConquerorData.ZoneDatas[cZoneSelect].FirstUnlock);
        else
            newNextZoneGO.SetActive(false);
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbar.transform.position = trans.position;
        lockbar.SetOriginPos(trans.position - Vector3.up * 1).SetContent($"Unlock at zone 6!", 0.5f).Show();
    }
    private void OpenSettingPopup() {
        offlineSettingText.gameObject.SetActive(false);
        offlineBoard.gameObject.SetActive(false);
        RemoveCanvasOffline();
        PopupHUD.Instance.Show<SettingPopup>().AddOnClose(CloseOfflineBoard);
    }
    private void OpenProgressReward() {
        PopupHUD.Instance.Show<ZoneProgressPopup>().OnHidePopup(UpdateZoneRewardNotice);
    }
    private void ShowEnergyPlay() {
        if (energyPrice) {
            energyPrice.SetModel(GameResources.Instance.EnergyData.EnergyNeedToPlay).Show();
        }
    }
    private void PlayGame() {
        if (GameResources.Instance.EnergyData.EnoughEnergyToPlay) {
            HUDManager.IgnoreUserInput(true);
            GameResources.Instance.Inventory.Remove(GameResources.Instance.EnergyData.EnergyNeedToPlay);
            IngameData.currentGameMode = GameMode.Conqueror;
            IngameData.currentZoneIndex = cZoneSelect - 1;
            DOVirtual.DelayedCall(0.7f, () => {
                HUDManager.IgnoreUserInput(false);
                IngameData.PlayGame(GameMode.Conqueror, LoadingBackground);
            });
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.PlayConquerorMode, 1);
            EventDispatcher.Instance.Dispatch(EventKey.OnPlayConquerorMode);
        }
        else {
            PopupHUD.Instance.Show<MoreEnergyPopup>();
        }
    }
    private void OpenInfinityPanel() {

        if (GameResources.Instance.ConquerorData.UnlockZone > 4) {
            PanelHUD.Instance.Show<InfinityPanel>();
        }
        else {
            ShowLockBarNotify(playInfinityButton.transform);
        }
    }
    private void OpenBattlePass() {
        PanelHUD.Instance.Show<BattlePassPopup>();
    }
    private void OpenSmartOffer() {
        PopupHUD.Instance.Show<SmartOfferPopup>().Initialize();
    }
    private void OpenShipPackPopup() {
        if (GameResources.Instance.ShipPackData.Status())
            PopupHUD.Instance.Show<ShipPackPopup>();
        else
            shipPackButton.gameObject.SetActive(false);
    }
    private void PriviousZoneClick() {
        if (cZoneSelect <= 1)
            return;
        cZoneSelect--;
        UpdateUI();
    }
    private void NextZoneClick() {
        if (cZoneSelect >= unlockZone)
            return;
        GameResources.Instance.ConquerorData.ZoneDatas[cZoneSelect].SetFirstUnlock(false);
        cZoneSelect++;
        UpdateUI();

    }
    private void OpenDailyLoginPopup() {
        PopupHUD.Instance.Show<DailyLoginLayout>();
    }
    public void OpenDailyPacksPopup() {
        PopupHUD.Instance.Show<DailyPacksPopup>();
    }
    private void OpenRookieLoginPopup() {
        PopupHUD.Instance.Show<RookieLoginLayout>();
    }
    private void OpenAfkPopup() {
        PopupHUD.Instance.Show<AfkPopup>().UpdateUI(GameResources.Instance.AFK);
    }
    private void OpenMissionPopup() {
        PopupHUD.Instance.Show<MissionPopup>().OpenPage(true);
    }
    private void LoadingBackground() {
        ToolbarScaler.Instance.SetActive(false);
        HeadHUD.Instance.HideAll();
    }
    private void SetStatePlayGame() {
        playGameButton.SetState(cZoneSelect <= GameResources.Instance.ConquerorData.UnlockZone + 1);
    }
    private void OnEnergyChanged() {
        spreadEffect.UpdateUI(playGameButton.interactable);
    }
    private void UpdateZoneRewardNotice() {
        (int cLevelClam, int cZoneClam) = GameResources.Instance.LevelProgress.Datas.GetCurrentLevelClaimable();
        reachZoneText.text = cZoneClam > Constant.ZoneCount ? "Coming soon" : $"Reach {cZoneClam}-{cLevelClam}";
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
    }
    private void ShowOfflineBoard() {
        if (!Networks.IsInternetAvaiable && tutData.FinishAllTutorial) {
            offlineSettingText.gameObject.SetActive(!offlineShowed);
            offlineBoard.gameObject.SetActive(!offlineShowed);
            if (offlineShowed)
                RemoveCanvasOffline();
            else
                AddCanvasOffline();
        }
        else {
            CloseOfflineBoard();
        }
    }
    private void AddCanvasOffline() {
        if (settingButton.GetComponent<Canvas>() == null) {
            var canvas = settingButton.gameObject.AddComponent<Canvas>();
            settingButton.gameObject.AddComponent<GraphicRaycaster>();
            canvas.overrideSorting = true;
            canvas.sortingLayerName = GameSortingLayer.Tutorial;
            canvas.sortingOrder = 1;
        }
        if (offlineBackgroundButton.GetComponent<Canvas>() == null) {
            var canvas = offlineBackgroundButton.gameObject.AddComponent<Canvas>();
            offlineBackgroundButton.gameObject.AddComponent<GraphicRaycaster>();
            canvas.overrideSorting = true;
            canvas.sortingLayerName = GameSortingLayer.Tutorial;
            canvas.sortingOrder = 0;
        }
    }
    private void RemoveCanvasOffline() {
        if (settingButton.GetComponent<GraphicRaycaster>() != null) {
            Destroy(settingButton.GetComponent<GraphicRaycaster>());
        }
        if (settingButton.GetComponent<Canvas>() != null) {
            Destroy(settingButton.GetComponent<Canvas>());
        }
        if (offlineBackgroundButton.GetComponent<GraphicRaycaster>() != null) {
            Destroy(offlineBackgroundButton.GetComponent<GraphicRaycaster>());
        }
        if (offlineBackgroundButton.GetComponent<Canvas>() != null) {
            Destroy(offlineBackgroundButton.GetComponent<Canvas>());
        }
    }
    private void CloseOfflineBoard() {
        offlineSettingText.gameObject.SetActive(false);
        offlineBoard.gameObject.SetActive(false);
        if (!offlineShowed)
            SpecialTriggerSystem.Instance.Action();
        offlineShowed = true;
        RemoveCanvasOffline();
    }
    #endregion

    public void OpenCheat() {
        PopupHUD.Instance.Show<CheatPopup>();
    }
}
