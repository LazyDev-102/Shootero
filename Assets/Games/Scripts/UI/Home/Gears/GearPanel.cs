using GameSystem.Common.UI;
using DG.Tweening;
using Gemmob;
using Gemmob.Tutorial;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Gear_Data;

public class GearPanel : DOTweenFrame {
    #region Variables
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showLeftToRight;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation showRightToLeft;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideLeftToRight;
    [SerializeField] private GameSystem.Common.UI.DOTweenAnimation hideRightToLeft;
    [SerializeField] private TextMeshProUGUI attackValue;
    [SerializeField] private TextMeshProUGUI hpValue;
    [SerializeField] private TextMeshProUGUI noItemText;
    [SerializeField] private ButtonExplorer openShipButton;
    [SerializeField] private ButtonBase btnShowStats;
    [SerializeField] private GearEquipment gearEquipment;
    [SerializeField] private GearContainer gearContainer;
    [SerializeField] private GearMenuBarScaler gearMenuBar;
    [SerializeField] private GameObject backgroundNoItem;
    [SerializeField] private GameObject chooseDronePanel;
    [SerializeField] private ButtonExplorer chooseDroneLButton;
    [SerializeField] private ButtonExplorer chooseDroneRButton;
    [SerializeField] private ButtonExplorer chooseBackButton;
    [SerializeField] private ButtonExplorer skillsButton;

    private GearInventory gearInv;
    private TutorialSytemData tutData;

    private GearSoftData gearEquip;
    private int currentType = 0;//(int)GearType.All;
    private bool hasAssignTarget;
    private int oldAttackValue = 0;
    private int oldHpValue = 0;
    private List<GearSoftData> gearWeaponries;
    private List<GearSoftData> gearShields;
    private List<GearSoftData> gearCores;
    private List<GearSoftData> gearEngines;
    private List<GearSoftData> gearDrones;
    private List<GearSoftData> cGears;
    private GearSlotData currentGearSlot;

    public List<GearSoftData> GearWeaponries { get => gearWeaponries; }
    public List<GearSoftData> GearShields { get => gearShields; }
    public List<GearSoftData> GearCores { get => gearCores; }
    public List<GearSoftData> GearEngines { get => gearEngines; }
    public List<GearSoftData> GearDrones { get => gearDrones; }
    public GearSlotData CurrentGearSlot { get => currentGearSlot; }
    public List<GearSoftData> CGears { get => cGears; }
    #endregion

    #region Init
    private void Awake() {
        SetData();
        AddEvent();
        InitGearData();
        AssignGearMenuBar();
    }

    private void OnEnable() {
#if !CHEAT
        btnShowStats.gameObject.SetActive(false);
#endif
        skillsButton.gameObject.SetActive(tutData.CanActiveSkills());
        ToolbarScaler.Instance.SetActive(true);
        ShowTutorial();
    }
    private void SetData() {
        gearInv = GameResources.Instance.GearInventory;
        tutData = GameResources.Instance.TutorialSytemData;
    }
    private void AddEvent() {
        openShipButton.AddEvent(OpenShipClick);
        btnShowStats.AddEvent(ShowStatsClickedButton);
        chooseDroneLButton.AddEvent(OnChooseDroneLClick);
        chooseDroneRButton.AddEvent(OnChooseDroneRClick);
        chooseBackButton.AddEvent(OnChooseBackClick);
        skillsButton.AddEvent(OpenSkillsPopup);
    }
    private void InitGearData() {
        gearInv.SortByRarety();
        gearWeaponries = new List<GearSoftData>();
        gearShields = new List<GearSoftData>();
        gearCores = new List<GearSoftData>();
        gearEngines = new List<GearSoftData>();
        gearDrones = new List<GearSoftData>();
        gearWeaponries = gearInv.GetWeaponries();
        gearShields = gearInv.GetShields();
        gearCores = gearInv.GetCores();
        gearEngines = gearInv.GetEngines();
        gearDrones = gearInv.GetDrones();
        currentGearSlot = gearInv.WeaponrySlot;
        cGears = gearWeaponries;
        CheckNew();
    }
    private void AssignGearMenuBar() {
        gearMenuBar.Assign(UpdateGearContainer);
    }
    public void ChangeStatInfor() {
        if (!gameObject.activeInHierarchy)
            return;
        PlayerStatManager.Instance.LoadShipData();
        PlayerStatManager.Instance.LoadGearData();
        if (oldAttackValue == 0) {
            attackValue.text = PlayerStatManager.Instance.Damage.ToString();
            hpValue.text = PlayerStatManager.Instance.HP.ToString();
            oldAttackValue = PlayerStatManager.Instance.Damage;
            oldHpValue = PlayerStatManager.Instance.HP;
        }
        else {
            StartCoroutine(IETextCounter(attackValue, oldAttackValue, PlayerStatManager.Instance.Damage, () => oldAttackValue = PlayerStatManager.Instance.Damage));
            StartCoroutine(IETextCounter(hpValue, oldHpValue, PlayerStatManager.Instance.HP, () => oldHpValue = PlayerStatManager.Instance.HP));
        }
    }
    private IEnumerator IETextCounter(TextMeshProUGUI text, int current, int target, Action onComplete) {
        int start = current;
        float duration = 0.5f;
        for (float timer = 0; timer < duration; timer += Time.deltaTime) {
            float progress = timer / duration;
            current = (int)Mathf.Lerp(start, target, progress);
            text.text = current.ToString();
            yield return null;
        }
        text.text = target.ToString();
        onComplete?.Invoke();
    }
    private bool FinishTutorialEquipment() {
        return tutData.FinishTutorialEquipment;
    }
    private GameObject GetMenuBarTabTutorial() {
        if (gearWeaponries.Count != 0)
            return gearMenuBar.MenubarItems[0].gameObject;
        else if (gearShields.Count != 0)
            return gearMenuBar.MenubarItems[1].gameObject;
        else if (gearCores.Count != 0)
            return gearMenuBar.MenubarItems[2].gameObject;
        else if (gearEngines.Count != 0)
            return gearMenuBar.MenubarItems[3].gameObject;
        else if (gearDrones.Count != 0)
            return gearMenuBar.MenubarItems[4].gameObject;
        return null;
    }
    #endregion
    #region Tutorial
    private void ShowTutorial() {
        ShowEquipGearTut();
        ShowSkillsEquipTut();
    }
    private void ShowEquipGearTut() {
        DOVirtual.DelayedCall(0.5f, () => {
            if (!hasAssignTarget && !FinishTutorialEquipment()) {
                GameObject menuTarget = GetMenuBarTabTutorial();
                TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                        .AssignTarget(TutorialKey.TutorialEquipment, 1, menuTarget)
                                        .AssignTarget(TutorialKey.TutorialEquipment, 4, ToolbarScaler.Instance.GetTabObject(ToolBarType.Conqueror));

                hasAssignTarget = true;
                DOVirtual.DelayedCall(0.5f, () => menuTarget.GetComponent<Image>().SetAlpha(0.1f)).SetUpdate(true);
            }
        }).SetUpdate(true);
    }
    private void ShowSkillsEquipTut() {
        if (tutData.CanShowEquipSkillsTutorial()) {
            TutorialSystem.Instance.SetTimeActiveCanvas(0.1f)
                                    .AssignTarget(TutorialKey.TutorialEquipSkills, 1, skillsButton.gameObject);
        }
    }
    #endregion
    #region Function
    private void UpdateUI() {
        gearWeaponries = gearInv.GetWeaponries();
        gearShields = gearInv.GetShields();
        gearCores = gearInv.GetCores();
        gearEngines = gearInv.GetEngines();
        gearDrones = gearInv.GetDrones();
        ChangeStatInfor();
        gearEquipment.UpdateUI();
        UpdateGearContainer((GearMenuType)(currentGearSlot.GearType - 1));
    }
    private void UpdateGearContainer(GearMenuType type) {
        gearInv.SortByRarety();
        switch (type) {
            case GearMenuType.Weapon:
                gearWeaponries = gearInv.GetWeaponries();
                gearContainer.UpdateUI(gearWeaponries);
                cGears = gearWeaponries;
                UpdateEmptyGearItem(gearWeaponries == null || gearWeaponries.Count == 0, "weapon");
                break;
            case GearMenuType.Shield:
                gearShields = gearInv.GetShields();
                gearContainer.UpdateUI(gearShields);
                cGears = gearShields;
                UpdateEmptyGearItem(gearShields == null || gearShields.Count == 0, "shield");
                break;
            case GearMenuType.Core:
                gearCores = gearInv.GetCores();
                gearContainer.UpdateUI(gearCores);
                cGears = gearCores;
                UpdateEmptyGearItem(gearCores == null || gearCores.Count == 0, "core");
                break;
            case GearMenuType.Engine:
                gearEngines = gearInv.GetEngines();
                gearContainer.UpdateUI(gearEngines);
                cGears = gearEngines;
                UpdateEmptyGearItem(gearEngines == null || gearEngines.Count == 0, "engine");
                break;
            case GearMenuType.Drone:
                gearDrones = gearInv.GetDrones();
                gearContainer.UpdateUI(gearDrones);
                cGears = gearDrones;
                UpdateEmptyGearItem(gearDrones == null || gearDrones.Count == 0, "drone");
                break;
            default:
                break;
        }
        CheckNew();
    }
    private void UpdateEmptyGearItem(bool status, string content) {
        backgroundNoItem.SetActive(status);
        noItemText.text = $"You have no {content}.";
    }
    public void OnGearDetailClose(bool reload) {
        if (reload) {
            ChangeStatInfor();
            gearEquipment.UpdateUI(/*gearContainer*/);
            UpdateGearContainer(gearMenuBar.CType);
        }
    }
    private void OpenShipClick() {
        PanelHUD.Instance.Show<ShipPanel>(pauseCurrent: true);
    }
    private void ShowStatsClickedButton() {
        PopupHUD.Instance.Show<ShowStatPopup>();
    }
    public void SetCurrentGearSlot(GearSlotData gearSlot) {
        currentGearSlot = gearSlot;
    }
    public void ChooseEquipDrone(GearSoftData gearEquip) {
        this.gearEquip = gearEquip;
        SetChooseDroneGroup(true);
    }
    private void OnChooseDroneLClick() {
        gearInv.DroneLSlot.UnEquipItem();
        gearInv.DroneLSlot.EquipItem(gearEquip);
        gearEquip.SetGearTypeSoft(GearType.Drone1);
        SetChooseDroneGroup(false);
        //UpdateUI();
        OnGearDetailClose(true);
    }
    private void OnChooseDroneRClick() {
        gearInv.DroneRSlot.UnEquipItem();
        gearInv.DroneRSlot.EquipItem(gearEquip);
        gearEquip.SetGearTypeSoft(GearType.Drone2);
        SetChooseDroneGroup(false);
        //UpdateUI();
        OnGearDetailClose(true);
    }
    private void OnChooseBackClick() {
        SetChooseDroneGroup(false);
        gearEquip = null;
    }
    private void OpenSkillsPopup() {
        PanelHUD.Instance.Show<SkillsPopup>(pauseCurrent: true);
    }
    private void SetChooseDroneGroup(bool status) {
        chooseDronePanel.SetActive(status);
        chooseBackButton.gameObject.SetActive(status);
        chooseDroneLButton.gameObject.SetActive(status);
        chooseDroneRButton.gameObject.SetActive(status);
    }

    #endregion

    #region Action Dotween Frame
    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        UpdateUI();
        GearMenuType type = currentGearSlot.IsDroneSlot ? GearMenuType.Drone : (GearMenuType)(currentGearSlot.GearType - 1);
        gearMenuBar.OnTabClick(type);
    }
    protected override void OnPause(Action onCompleted = null, bool instant = false) {
        base.OnPause(onCompleted, instant);
        gameObject.SetActive(false);
    }

    protected override void OnResume(Action onCompleted = null, bool instant = false) {
        base.OnResume(onCompleted, instant);
        gameObject.SetActive(true);
        ChangeStatInfor();
    }
    private void CheckNew() {
        foreach (var g in cGears) {
            g.CheckNew();
        }
    }
    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        //List<GearSoftData> gears = GameResourcesIG.Instance.GearInventory.GearItems;
        //foreach (var g in gears) {
        //    g.CheckNew();
        //}
        GetComponent<CanvasGroup>().alpha = 1;
        EventDispatcher.Instance.Dispatch(EventKey.OnAllNewGearChecked);
    }
    public override Frame SetAnimShow(bool leftToRight) {
        showAnimation = leftToRight ? showLeftToRight : showRightToLeft;
        return this;
    }
    public override Frame SetAnimHide(bool leftToRight) {
        hideAnimation = leftToRight ? hideLeftToRight : hideRightToLeft;
        return this;
    }
    #endregion
}
