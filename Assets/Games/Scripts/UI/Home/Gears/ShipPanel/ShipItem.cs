using Gemmob;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipItem : ItemBase<ShipInfor> {
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI shipNameText;
    [SerializeField] private TextMeshProUGUI shipLevelText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject tick;
    [SerializeField] private GameObject levelGO;
    [SerializeField] private Image upGO;
    [SerializeField] private ButtonExplorer selectButton;
    [SerializeField] private Transform frame;
    [SerializeField] private Color levelColor;
    [SerializeField] private Material blackMat;
    [SerializeField] private Material enableMat;
    [SerializeField] private Material disableMat;
    [SerializeField] private SelectShipNotiListener selectShipNoti;

    private ShipContainer shipContainer;
    private bool itemUnlock;
    private int levelUnlock;
    private void Awake() {
        selectButton.AddEvent(OnSelectItem);
        EventDispatcher.Instance.AddListener<EventKey.OnBuyShip>(UpdateUIWhenBuyShip);
        EventDispatcher.Instance.AddListener<EventKey.OnEnhanceShip>(UpdateUIWhenEnhanceShip);
        EventDispatcher.Instance.AddListener<EventKey.OnShipChange>(UpdateUIWhenEquipShip);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnBuyShip>(UpdateUIWhenBuyShip);
        EventDispatcher.Instance.RemoveListener<EventKey.OnEnhanceShip>(UpdateUIWhenEnhanceShip);
        EventDispatcher.Instance.RemoveListener<EventKey.OnShipChange>(UpdateUIWhenEquipShip);
    }
    private ShipInfor data;
    public override void UpdateUI(ContainerBase<ShipInfor> view, ShipInfor data) {
        base.UpdateUI(view, data);
        this.data = data;
        selectButton.SetState(!data.ComingSoon);
        (itemUnlock, levelUnlock) = GetLevelUnlock();
        shipContainer = (ShipContainer)view;
        UpdateUI();
        if (data.ID == GameResources.Instance.Ship.CurrentShip)
            OnSelectItem();
    }
    public void SetFrame() {
        frame.SetParent(transform);
        frame.localPosition = Vector3.zero;
        frame.localScale = Vector3.one;
        frame.SetAsLastSibling();
    }
    private void UpdateUI() {
        icon.sprite = data.GetIcon();
        shipNameText.text = data.Name;
        shipLevelText.text = $"{data.CurrentLevel + 1}/{data.Levels.Count}";
        shipLevelText.color = data.IsMax ? Color.white : levelColor;
        upGO.color = data.IsMax ? Color.white : levelColor;
        upGO.gameObject.SetActive(!data.IsMax);
        locked.SetActive(!data.Unlocked);
        levelGO.SetActive(data.Unlocked);
        tick.SetActive(data.ID == GameResources.Instance.Ship.CurrentShip);
        questionText.gameObject.SetActive(data.ComingSoon);
        SetStateSelect();
        selectShipNoti?.CheckToShow(data);
    }
    private void UpdateUIWhenBuyShip(EventKey.OnBuyShip ship) {
        if (ship.ID == data.ID)
            UpdateUI();
    }
    private void UpdateUIWhenEnhanceShip(EventKey.OnEnhanceShip ship) {
        if (ship.ID == data.ID)
            UpdateUI();
    }
    private void UpdateUIWhenEquipShip(EventKey.OnShipChange shipID) {
        tick.SetActive(data.ID == GameResources.Instance.Ship.CurrentShip);
    }
    private void OnSelectItem() {
        if (!itemUnlock) {
            shipContainer.ShowNotify(levelUnlock, transform);
            return;
        }
        PlayerStatManager.Instance.LoadPassive(data);
        SetFrame();
        data.IsOpenChecked = true;
        selectShipNoti?.SetNotificationGraphicState(false);
        EventDispatcher.Instance.Dispatch(new EventKey.OnSelectShip { ID = data.ID });
    }
    private void SetStateSelect() {
        selectButton.SetState(!data.ComingSoon);
        icon.material = data.ComingSoon ? blackMat : enableMat;
        icon.color = data.ComingSoon ? Color.black : Color.white;
        //icon.material = itemUnlock ? enableMat : disableMat;
        //switch (data.ID) {
        //    case 1:
        //        selectButton.SetState(!data.ComingSoon);
        //        icon.material = itemUnlock ? enableMat : disableMat;
        //        break;
        //    case 2:
        //        selectButton.SetState(!data.ComingSoon);
        //        icon.material = itemUnlock ? enableMat : disableMat;
        //        break;
        //    case 3:
        //        selectButton.SetState(!data.ComingSoon);
        //        icon.material = itemUnlock ? enableMat : disableMat;
        //        break;
        //    case 4:
        //        selectButton.SetState(!data.ComingSoon);
        //        icon.material = itemUnlock ? enableMat : disableMat;
        //        break;
        //    case 5:
        //        selectButton.SetState(!data.ComingSoon);
        //        icon.material = itemUnlock ? enableMat : disableMat;
        //        break;
        //    case 6:
        //        selectButton.SetState(!data.ComingSoon);
        //        icon.material = blackMat;
        //        icon.color = Color.black;
        //        break;

        //}
    }

    private (bool, int) GetLevelUnlock() {
        int levelProgress = GameResources.Instance.LevelProgress.GetCurrentLevel() + 1;
        return (data.CanUnlock(levelProgress), data.CanUnlockLevel);
        //switch (data.ID) {
        //    case 1:
        //        return (true, 0);
        //    case 2:
        //        return (GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.CanUnlockUnlockShip2(GameResourcesIG.Instance.LevelProgress.GetCurrentLevel() + 1), GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.GetLevelUnlockShip2());
        //    case 3:
        //        return (GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.CanUnlockUnlockShip3(GameResourcesIG.Instance.LevelProgress.GetCurrentLevel() + 1), GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.GetLevelUnlockShip3());
        //    case 4:
        //        return (GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.CanUnlockUnlockShip4(GameResourcesIG.Instance.LevelProgress.GetCurrentLevel() + 1), GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.GetLevelUnlockShip4());
        //    case 5:
        //        return (GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.CanUnlockUnlockShip5(GameResourcesIG.Instance.LevelProgress.GetCurrentLevel() + 1), GameResourcesIG.Instance.LevelProgress.Datas.UnlockFeatures.GetLevelUnlockShip5());
        //}
        //return (false, 100);
    }
}
