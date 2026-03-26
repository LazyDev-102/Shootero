using GameSystem.Common.UI;
using Gemmob;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipPanel : DOTweenFrame {
    #region Variables
    [SerializeField] float timeScaleText = 0.2f;
    [SerializeField] private Image shipIcon;
    [SerializeField] private Image unlockIcon;
    [SerializeField] private Image specialExtIcon;
    [SerializeField] private HoldButton infoShipButton;
    [SerializeField] private ButtonExplorer backButton;
    [SerializeField] private ButtonExplorer buyShipButton;
    [SerializeField] private ButtonExplorer enhanceButton;
    [SerializeField] private ButtonExplorer equipButton;
    [SerializeField] private ButtonExplorer trialButton;
    [SerializeField] private TextMeshProUGUI equipText;
    [SerializeField] private TextMeshProUGUI shipNameText;
    [SerializeField] private TextMeshProUGUI shipDescriptionText;
    [SerializeField] private TextMeshProUGUI shipExtDescriptionText;
    [SerializeField] private TextMeshProUGUI shipLevelText;
    [SerializeField] private TextMeshProUGUI shipAttackValueText;
    [SerializeField] private TextMeshProUGUI shipHPValueText;
    [SerializeField] private TextColorSystem priceUnlockText;
    [SerializeField] private LockbarNotify lockbarNotify;
    [SerializeField] private BuyShipNotiListener buyShipNoti;
    [SerializeField] private EnhanceShipNotiListener enhanceShipNoti;
    [SerializeField] private ShipContainer shipContainer;
    [SerializeField] private ParticleSystem enhanceEffect;
    [SerializeField] private GameObject shipInfoPanel;
    [SerializeField] private GameObject unlockGroup;
    [SerializeField] private GameObject enhanceGroup;

    private LevelProgressData levelProgress;
    private ShipInfor shipInfo;
    private ShipData shipData;
    private bool canEnhance;
    #endregion

    #region Init
    private void Awake() {
        backButton.AddEvent(OnClose);
        buyShipButton.AddEvent(BuyShip);
        enhanceButton.AddEvent(EnhanceShip);
        equipButton.AddEvent(EquipShip);
        trialButton.AddEvent(OnTryShip);
        infoShipButton.AddHoldEvent(ShowInfoShip);
        infoShipButton.AddReleaseEvent(HideInfoShip);
        shipContainer.SetParent(this);
        shipData = GameResources.Instance.Ship;
        levelProgress = GameResources.Instance.LevelProgress;
        EventDispatcher.Instance.AddListener<EventKey.OnSelectShip>(OnSelectShip);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnSelectShip>(OnSelectShip);
    }
    private void OnEnable() {
        UpdateUI();
        shipData.SetSaw(true);
        ToolbarScaler.Instance.SetActive(false);
    }
    #endregion

    #region Function
    private void UpdateUI() {
        shipContainer.Reload();
        lockbarNotify.gameObject.SetActive(false);
        UpdateInforShip(shipData.CurrentShip);
    }
    private void UpdateInforShip(int shipID) {
        shipInfo = shipData.GetShipInfor(shipID);
        SetShipInfoUI(shipInfo);
        SetButtonStatus(shipInfo);
        SetStateEnhance(shipInfo);
        if (shipInfo.Unlocked)
            SetShipPowerUnlocked(shipInfo);
        else
            SetShipPowerLocked(shipInfo);
        CheckNotify(shipInfo);
    }
    private void SetShipInfoUI(ShipInfor shipInfo) {
        shipIcon.sprite = shipInfo.GetIcon();
        specialExtIcon.sprite = shipInfo.ExtIcon;
        shipNameText.text = shipInfo.Name;
        shipDescriptionText.text = shipInfo.Description;
        shipExtDescriptionText.text = shipInfo.GetCurrentSpecialText();
    }
    private void SetShipPowerUnlocked(ShipInfor shipInfo) {
        var level = shipInfo.CurrentLevel + 1;
        shipLevelText.text = $"Level {level}";
        shipAttackValueText.text = $"{shipInfo.GetDamage()}";
        shipHPValueText.text = $"{shipInfo.GetHP()}";
    }
    private void SetShipPowerLocked(ShipInfor shipInfo) {
        var price = shipInfo.Levels[0].Price;
        shipLevelText.text = "Level 1";
        priceUnlockText.SetData(price.Amount, (CurrencyType)price.Id);
        unlockIcon.sprite = shipInfo.GetUnlockIcon();
        buyShipButton.SetState(!shipInfo.ComingSoon);
        shipAttackValueText.text = $"{shipInfo.GetCurrentAttack()}";
        shipHPValueText.text = $"{shipInfo.GetCurrentHP()}";
    }
    private void SetButtonStatus(ShipInfor shipInfo) {
        var equipState = shipInfo.ID != shipData.CurrentShip;
        bool canUnlock = shipInfo.CanUnlock(levelProgress.GetCurrentLevel() + 1);
        bool unlocked = shipInfo.Unlocked;
        unlockGroup.SetActive(!unlocked);
        enhanceGroup.SetActive(unlocked);
        canEnhance = unlocked && !shipInfo.IsMax;
        buyShipButton.gameObject.SetActive(!unlocked);
        enhanceButton.gameObject.SetActive(canEnhance);
        equipButton.gameObject.SetActive(unlocked);
        equipButton.SetState(equipState);
        equipButton.SetIconStatus(equipState);
        equipText.SetText(equipState ? "EQUIP" : "EQUIPPED");
        trialButton.gameObject.SetActive(!unlocked && canUnlock);
    }
    private void CheckNotify(ShipInfor shipInfo) {
        enhanceShipNoti?.CheckToShow(shipInfo);
        buyShipNoti?.CheckToShow(shipInfo);
    }
    private void OnSelectShip(EventKey.OnSelectShip ship) {
        UpdateInforShip(ship.ID);
    }
    private void BuyShip() {
        var price1 = shipInfo.Levels[0].Price;
        var inv = GameResources.Instance.Inventory;
        if (inv.GetItem(price1.Id).Amount >= price1.Amount) {
            ItemStack price = shipInfo.Levels[0].Price;
            ItemStack curCurrency = inv.GetItem(price.Id);
            if (curCurrency.Amount >= price.Amount) {
                if (shipData.BuyShip(shipInfo.ID)) {
                    inv.Add(price.Id, -price.Amount);
                    EventDispatcher.Instance.Dispatch(new EventKey.OnBuyShip() { ID = shipInfo.ID });
                    UpdateInforShip(shipInfo.ID);
                    Tracking.Instance.LogShip($"{shipInfo.ID}", shipInfo.CurrentLevel);
                    return;
                }
            }
        }
        else {
            ShowLockBarNotify(buyShipButton.transform);
        }
    }
    private void EnhanceShip() {
        PopupHUD.Instance.Show<ShipEnhancePopup>()
                         .AddOnClose(UpdateUI)
                         .UpdateUI(shipInfo);
    }
    private void EquipShip() {
        if (!shipData.SetCurrentShip(shipInfo.ID)) {
            UpdateInforShip(shipInfo.ID);
        }
        else {
            shipData.SetCurrentShip(shipInfo.ID);
            UpdateInforShip(shipInfo.ID);
            EventDispatcher.Instance.Dispatch(new EventKey.OnShipChange() { shipID = shipInfo.ID });
        }
    }
    private void OnTryShip() {
        shipData.SetTrial(true, shipInfo.ID);
        IngameData.currentGameMode = GameMode.Conqueror;
        SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.Tutorial);
    }
    private void OnClose() {
        Hide();
        ToolbarScaler.Instance.SetActive(true);
    }

    private void SetStateEnhance(ShipInfor shipInfor) {
        if (shipInfor.IsMax) {
            enhanceButton.SetState(false);
            return;
        }
        var currentLevel = levelProgress.GetCurrentLevel();
        var unlockFeature = levelProgress.Datas.UnlockFeatures.CanUnlockEnhanceShip(currentLevel + 1);
        enhanceButton.SetState(unlockFeature);
    }

    public void ShowLockBarNotify(int level, Transform shipItemTrans) {
        lockbarNotify.transform.position = shipItemTrans.position;
        lockbarNotify.SetOriginPos(shipItemTrans.position - Vector3.up * 1).SetContent($"Unlock at !", 0.5f, true, level.ToString()).Show();
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
    private void ShowInfoShip() {
        if (shipInfoPanel != null)
            shipInfoPanel.SetActive(true);
    }
    private void HideInfoShip() {
        if (shipInfoPanel != null)
            shipInfoPanel.SetActive(false);
    }
    #endregion
}
