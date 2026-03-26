using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Gemmob;
using GameSystem.Common.UI;

public class ToolbarScaler : SingletonBind<ToolbarScaler> {
    [SerializeField] private ButtonExplorer[] tabs;
    [SerializeField] private float diffScale = 100, diffHigh = 40;
    [SerializeField] private List<ToolBarItem> items;
    [SerializeField] private LockbarNotify lockBar;
    [SerializeField] private Material disableMat;
    [SerializeField] private Material enableMat;
    [SerializeField] private Transform frameSelect;
    [SerializeField] private GameObject background;
    [SerializeField] private UINotiListeners shopListener;
    [SerializeField] private NotiListener abilityListener;
    [SerializeField] private AnimationCurve iconMoveCurve;

    private float originY;
    private ToolBarType cType = ToolBarType.Conqueror;
    private bool canOpenAblility;
    private bool canOpenInfinity;
    private bool canOpenShop;

    private LevelProgressData levelData;
    public LockbarNotify LockBar { get => lockBar; }

    private void Start() {
        SetData();
        AssignUI();
        AssignEvent();
        ShopNotify();
        EventDispatcher.Instance.AddListener<EventKey.OnExpChange>(UpdateStateTab);
    }
    protected override void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnExpChange>(UpdateStateTab);
    }

    private void SetData() {
        if (levelData == null)
            levelData = GameResources.Instance.LevelProgress;
    }
    private void AssignUI() {
        originY = items[0].GetOriginPosY();
        lockBar.SetOriginPos(transform.position + Vector3.up);
        PanelHUD.Instance.Show<ConquerorPanel>();
        items[(int)ToolBarType.Conqueror].MoveUpIcon(true, originY, diffHigh, iconMoveCurve)
                                         .SetActiveName(true)
                                         .ChangeIcon(true);
    }
    private void AssignEvent() {
        tabs[(int)ToolBarType.Shop].AddEvent(ShowShopPanel);
        tabs[(int)ToolBarType.Gears].AddEvent(ShowGearPanel);
        tabs[(int)ToolBarType.Conqueror].AddEvent(ShowConquerorPanel);
        tabs[(int)ToolBarType.Ability].AddEvent(ShowAbilityPanel);
        tabs[(int)ToolBarType.Infinity].AddEvent(ShowModesPanel);
        UpdateStateTab();
    }
    public void UpdateStateTab() {
        canOpenAblility = levelData.Datas.UnlockFeatures.CanUnlockAbility(levelData.GetCurrentLevel() + 1);
        canOpenInfinity = levelData.Datas.UnlockFeatures.CanUnlockInfinityMode(levelData.GetCurrentLevel() + 1);
        canOpenShop = levelData.Datas.UnlockFeatures.CanUnlockShop(levelData.GetCurrentLevel() + 1);
        items[0].SetLockTab(!canOpenShop).SetMaterial(canOpenShop ? enableMat : disableMat).SetAlpha(canOpenShop ? 1 : 0.5f);
        items[3].SetLockTab(!canOpenAblility).SetMaterial(canOpenShop ? enableMat : disableMat).SetAlpha(canOpenAblility ? 1 : 0.5f);
        items[4].SetLockTab(!canOpenInfinity).SetMaterial(canOpenShop ? enableMat : disableMat).SetAlpha(canOpenInfinity ? 1 : 0.5f);
    }

    public void ShowConquerorPanel() {
        if (cType == ToolBarType.Conqueror)
            return;
        MoveFrameSelect((int)ToolBarType.Conqueror);
        HideCurrentPanel(ToolBarType.Conqueror);
        PanelHUD.Instance.Conqueror.SetAnimShow(cType > ToolBarType.Conqueror);
        PanelHUD.Instance.Show<ConquerorPanel>();
        cType = ToolBarType.Conqueror;
    }
    public void ShowShopPanel() {
        if (!canOpenShop) {
            lockBar.SetContent($"Coming soon!", 0.5f).Show();
            return;
        }
        if (cType == ToolBarType.Shop)
            return;
        MoveFrameSelect((int)ToolBarType.Shop);
        HideCurrentPanel(ToolBarType.Shop);
        PanelHUD.Instance.Shop.SetAnimShow(cType > ToolBarType.Shop);
        PanelHUD.Instance.Show<ShopPanel>();
        cType = ToolBarType.Shop;
    }
    public void ShowGearPanel() {
        if (cType == ToolBarType.Gears)
            return;
        MoveFrameSelect((int)ToolBarType.Gears);
        HideCurrentPanel(ToolBarType.Gears);
        PanelHUD.Instance.Gear.SetAnimShow(cType > ToolBarType.Gears);
        PanelHUD.Instance.Show<GearPanel>();
        cType = ToolBarType.Gears;
    }
    public void ShowAbilityPanel() {
        if (!canOpenAblility) {
            int level = levelData.Datas.UnlockFeatures.GetlevelUnlockAbility();
            lockBar.SetContent($"Unlock at ", 0.5f, true, level.ToString()).Show();
            //lockBar.SetContent($"Coming soon!", 0.5f).Show();
            return;
        }
        if (cType == ToolBarType.Ability)
            return;
        MoveFrameSelect((int)ToolBarType.Ability);
        HideCurrentPanel(ToolBarType.Ability);
        PanelHUD.Instance.Ability.SetAnimShow(cType > ToolBarType.Ability);
        PanelHUD.Instance.Show<NewAbilityPanel>();
        cType = ToolBarType.Ability;
    }

    public void ShowModesPanel() {
        if (!canOpenInfinity) {
            int level = levelData.Datas.UnlockFeatures.GetlevelUnlockInfinityMode();
            //lockBar.SetContent($"Coming soon!", 0.5f).Show();
            lockBar.SetContent($"Unlock at ", 0.5f, true, level.ToString()).Show();
            return;
        }
        if (cType == ToolBarType.Infinity)
            return;
        MoveFrameSelect((int)ToolBarType.Infinity);
        HideCurrentPanel(ToolBarType.Infinity);
        PanelHUD.Instance.Modes.SetAnimShow(cType > ToolBarType.Infinity);
        PanelHUD.Instance.Show<ModesPanel>();
        cType = ToolBarType.Infinity;
    }

    private void HideCurrentPanel(ToolBarType type) {
        DOTweenFrame cFrame = PanelHUD.Instance.GetFrameOnTop() as DOTweenFrame;
        if (cFrame) {
            cFrame.SetAnimHide(cType > type);
        }
        PanelHUD.Instance.Hide();
    }

    public void MoveFrameSelect(int index) {
        frameSelect.DOKill(false);
        frameSelect.DOLocalMoveX(tabs[index].transform.localPosition.x, 0.2f).SetEase(Ease.Linear).SetUpdate(true);
        for (int i = 0; i < items.Count; i++) {
            items[i].MoveUpIcon(i == index, originY, diffHigh, iconMoveCurve)
                    .SetActiveName(i == index)
                    .ChangeIcon(i == index);
        }
    }

    public void SetActive(bool active) {
        gameObject.SetActive(active);
        frameSelect.gameObject.SetActive(active);
        background.SetActive(active);
    }

    public void ShopNotify() {
        if (gameObject.activeInHierarchy && shopListener)
            shopListener.CheckToShow();
    }

    public void AbilityCheckNotify() {
        if (gameObject.activeInHierarchy && abilityListener)
            abilityListener.CheckToShow();
    }

    public GameObject GetTabObject(ToolBarType type) {
        return tabs[(int)type].gameObject;
    }
}
