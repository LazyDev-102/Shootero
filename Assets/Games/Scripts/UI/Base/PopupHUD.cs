using GameSystem.Common.UI;
using Gemmob;
using UnityEngine;

public class PopupHUD : HUD<PopupHUD> {

    #region Ingame Popup Varialbles
    private MissionPopup mission;
    private DailyPacksPopup dailyPacks;
    private CheatPopup cheat;
    private RewardPopup reward;
    private AssignGearPopup assignGear;
    private GearSlotItemDetailPopup gearSlotItemDetail;
    private TutorialIntroducePopup tutorialIntroduce;
    private RookieLoginLayout rookieLogin;
    private DailyLoginLayout dailyLogin;
    private ConfirmPopup confirm;
    private SettingPopup setting;
    private ZoneProgressPopup zoneProgress;
    private LevelUpPopup levelUp;
    private GearMatrialDetailPopup gearMatrial;
    private UpgradeAbilityPopup upgradeAbility;
    private GearDetailItemPopup gearDetailItem;
    private MoreEnergyPopup moreEnergy;
    private ShowStatPopup showStat;
    private OpenChestPopup openChest;
    private ChooseModPopup chooseMod;

    protected override void Awake() {
        base.Awake();
        EventDispatcher.Instance.AddListener(EventKey.OnLevelSystemUp, OnLevelSystemUp);
    }
    protected override void OnDestroy() {
        base.OnDestroy();
        EventDispatcher.Instance.RemoveListener(EventKey.OnLevelSystemUp, OnLevelSystemUp);
    }
    public AssignGearPopup AssignGearActive {
        get {
            return GetActiveFrame<AssignGearPopup>();
        }
    }
    public GearSlotItemDetailPopup GearSlotItemDetail {
        get {
            return GetActiveFrame<GearSlotItemDetailPopup>();
        }
    }
    public GearDetailItemPopup GearDetailItemPopup {
        get {
            return GetFrame<GearDetailItemPopup>();
        }
    }
    public MissionPopup Mission {
        get {
            mission = GetActiveFrame<MissionPopup>();
            if (mission == null) {
                mission = GetFrame<MissionPopup>();
            }
            return mission;
        }
    }
    public RewardPopup Reward {
        get {
            reward = GetActiveFrame<RewardPopup>();
            if (reward == null) {
                reward = GetFrame<RewardPopup>();
            }
            return reward;
        }
    }
    public ConfirmPopup Confirm {
        get {
            confirm = GetActiveFrame<ConfirmPopup>();
            if (confirm == null) {
                confirm = GetFrame<ConfirmPopup>();
            }
            return confirm;
        }
    }
    public ZoneProgressPopup ZoneProgress {
        get {
            zoneProgress = GetActiveFrame<ZoneProgressPopup>();
            if (zoneProgress == null) {
                zoneProgress = GetFrame<ZoneProgressPopup>();
            }
            return zoneProgress;
        }
    }
    public LevelUpPopup LevelUp {
        get {
            levelUp = GetActiveFrame<LevelUpPopup>();
            if (levelUp == null) {
                levelUp = GetFrame<LevelUpPopup>();
            }
            return levelUp;
        }
    }
    public MoreEnergyPopup MoreEnergy {
        get {
            moreEnergy = GetActiveFrame<MoreEnergyPopup>();
            if (moreEnergy == null) {
                moreEnergy = GetFrame<MoreEnergyPopup>();
            }
            return moreEnergy;
        }
    }
    public OpenChestPopup OpenChest {
        get {
            openChest = GetActiveFrame<OpenChestPopup>();
            if (openChest == null) {
                openChest = GetFrame<OpenChestPopup>();
            }
            return openChest;
        }
    }
    public ChooseModPopup ChooseMod {
        get {
            chooseMod = GetActiveFrame<ChooseModPopup>();
            if (chooseMod == null) {
                chooseMod = GetFrame<ChooseModPopup>();
            }
            return chooseMod;
        }
    }

    #endregion
    public override void Back() {
        var pause = GetFrameOnTop<PausePopup>();
        if (pause != null) {
            pause.OnBack();
            return;
        }
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment)
            return;
        base.Back();
    }

    public void ShowConfirm(System.Action successAction, System.Action failAction, string title = "", string content = "", string btnConfirmTitle = "", string btnCancelTitle = "", bool hideOnYes = true, bool hideOnNo = true, bool btnClose = false) {
        Confirm.transform.SetAsLastSibling();
        Confirm.Init(successAction, failAction, title, content, btnConfirmTitle, btnCancelTitle, hideOnYes, hideOnNo, btnClose);
        Show<ConfirmPopup>();
    }
    private void OnLevelSystemUp() {
        if (GameManager.Initialized)
            return;
        Show<LevelUpPopup>().SetData().AddOnClose(LevelUp.CheckShowAgain).Show();
    }
}
