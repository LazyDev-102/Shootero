
using UnityEngine;
using UnityEngine.UI;

public class GearItemView : MonoBehaviour, IItem<GearSoftData> {
    [SerializeField] private Image icon;
    [SerializeField] private Image frame;
    [SerializeField] private GameObject notification;
    [SerializeField] private GameObject tick;
    [SerializeField] private GameObject equippedGO;
    [SerializeField] private GameObject upgradeableGO;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private NewGearNotiListener notiListener;
    private bool isPassive;

    private void Awake() {
        selectButton?.AddEvent(OnSelectItem);
    }

    public GearSoftData dataStack { get; set; }

    public void UpdateUI() {
        icon.sprite = dataStack.GearHardData.GetIcon(dataStack.CurrentRank);
        frame.sprite = dataStack.GearHardData.GetRarety(dataStack.CurrentRank).Frame;
        if (equippedGO != null)
            equippedGO.SetActive(dataStack.IsEquiped);
        ShowUpgradeableGO();
        SetNotification();
    }
    public void UpdateUI(GearSoftData data, bool isPassive = false) {
        this.dataStack = data;
        this.isPassive = isPassive;
        UpdateUI();
    }
    private void SetNotification() {
        if (notification != null)
            notification.SetActive(GameResources.Instance.GearInventory.GearHasCombo(dataStack.GearHardData.Id, dataStack.CurrentRank));
        if (notiListener) {
            notiListener.CheckToShow(dataStack);
        }
    }
    public void SetTick(bool active) {
        tick?.SetActive(active);
    }
    private void OnSelectItem() {
        PopupHUD.Instance.Show<GearDetailItemPopup>().InitData(dataStack, this, isPassive, false, null);
    }

    public GearType GetGearType() {
        return dataStack.GearHardData.GearType;
    }

    private void ShowUpgradeableGO() {
        if (upgradeableGO != null) {
            var levelItem = dataStack.CurrentLevel;
            var item = dataStack.GearHardData.Levels[levelItem - 1];
            var matLeft = GameResources.Instance.Inventory.GetItem(GetID(dataStack.GearHardData.GearType)).Amount;
            var levelPlayer = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
            var required = GameResources.Instance.LevelProgress.Datas.UnlockFeatures.CanUnlockEnhanceGear(levelPlayer);
            var matNeed = dataStack.GearHardData.Levels[levelItem].EnhanceRequire[0].Amount;
            var currencyNeed = item.PriceUpgrade.Amount;
            var currencyLeft = GameResources.Instance.Inventory.GetItem(item.PriceUpgrade.Id).Amount;
            upgradeableGO.SetActive(currencyLeft >= currencyNeed && matLeft >= matNeed && required && levelPlayer > levelItem);
        }
    }

    private int GetID(GearType type) {
        switch (type) {
            case GearType.Weapon:
                return ConstantItemID.WeaponryMatId;
            case GearType.Shield:
                return ConstantItemID.ShieldMatId;
            case GearType.Reactor:
                return ConstantItemID.ReatorMatId;
            case GearType.Propulsion:
                return ConstantItemID.PropulsionMatId;
            case GearType.Drone1:
            case GearType.Drone2:
                return ConstantItemID.DroneMatId;
            default:
                return ConstantItemID.WeaponryMatId;
        }
    }

    public IItem<GearSoftData> Generate() {
        throw new System.NotImplementedException();
    }
}
