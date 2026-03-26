using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ChestItemDisplayer : View<ChestItem> {
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI txtSpecialDescription;
    [SerializeField] private TextMeshProUGUI txtSpecialGearType;
    [SerializeField] private TextMeshProUGUI txtTimeFree;
    [SerializeField] private Image imgIcon;
    [SerializeField] private ButtonBase btnAds;
    [SerializeField] private ButtonBase btnPrice;
    [SerializeField] private ButtonBase btnKey;
    [SerializeField] private ItemView priceView;
    [SerializeField] private CurrentItemView keyView;
    [SerializeField] private Timer freeTimer;
    [SerializeField] private ParticleSystem effect;
    [SerializeField] private GameObject timeFreeGO;
    [SerializeField] private GameObject gearSpecialGO;
    [SerializeField] private LockbarNotify lockbar;

    public ButtonBase BtnKey { get => btnKey; }

    private void Start() {
        btnAds?.AddEvent(OnButtonAdsClicked);
        btnPrice?.AddEvent(OnButtonPriceClicked);
        btnKey?.AddEvent(OnButtonKeyClicked);
    }

    public override void Show() {
        if (Model == null) {

            return;
        }
        SetContentName(Model.Name, true);
        SetContentDesription(Model.Description, true);
        SetContentSpecialDescription(Model.GetSpecialDescription, true);
        SetContentSpecialDescription(Model.CurrentNumberSpectialOpen == 1, Model.GetSpecialTypeDescription);
        SetIcon(Model.Icon, true);
        ItemStack keyItem = Model.KeyOpen;
        ItemStack curKeyItem = GameResources.Instance.Inventory.GetItem(keyItem.Id);
        bool hasFree = Model.IsGetFreeReady();
        LoadFree();
        ItemStack price = Model.Price;
        if (Model.IsOpenFreeWithAds == false && hasFree) {
            Model.ResetFreeOpen();
            Model.AddKey(1);
        }
        if (!GameResources.Instance.TutorialSytemData.FinishTutorialEquipment) {
            bool hasOpenKey = curKeyItem.Amount > 0;
            bool hasOpenAds = !hasOpenKey && Model.IsOpenFreeWithAds && hasFree && Application.internetReachability != NetworkReachability.NotReachable && EMAdManager.Instance.HasRewardAds();
            bool hasOpenPrice = !hasOpenAds && !hasOpenKey;
            SetStateAdsButton(true, hasOpenAds);
            SetStateKeyButton(true, hasOpenKey);
            SetStatePriceButton(true, hasOpenPrice);
            SetPriceView(price, hasOpenPrice);
            SetKeyView(keyItem, hasOpenKey);
        }
        else {
            bool hasOpenAds = Model.IsOpenFreeWithAds && hasFree && Application.internetReachability != NetworkReachability.NotReachable && EMAdManager.Instance.HasRewardAds();
            bool hasOpenKey = !hasOpenAds && curKeyItem.Amount > 0;
            bool hasOpenPrice = !hasOpenAds && !hasOpenKey;
            SetStateAdsButton(true, hasOpenAds);
            SetStateKeyButton(true, hasOpenKey);
            SetStatePriceButton(true, hasOpenPrice);
            SetPriceView(price, hasOpenPrice);
            SetKeyView(keyItem, hasOpenKey);
        }
        if (effect) {
            effect.gameObject.SetActive(Model.ShowEffect);
        }
        lockbar.gameObject.SetActive(false);
    }

    private void LoadFree() {
        bool hasFree = Model.IsGetFreeReady();
        if (hasFree) {
            SetContentTimeFree(string.Empty, false);
        }
        else {
            DateTime current = DateTime.Now;
            DateTime ready = Model.GetFreeTimeReady;
            freeTimer.Countdown(ready - current
               , elapsed => {
                   SetContentTimeFree(GetContentFreeTime(elapsed), true);
               }, () => {
                   Show();
               }, true);
        }
    }

    private string GetContentFreeTime(TimeSpan timeSpan) {
        if (timeSpan.TotalDays >= 1) {
            return $"{timeSpan.Days}D {timeSpan.Hours}H";
        }
        else {
            return $"{timeSpan.Hours}H {timeSpan.Minutes}M";
        }
    }

    private void OnButtonAdsClicked() {
        bool isNormal = Model.KeyOpen.Id == ConstantItemID.NormalKey;
        EMAdManager.Instance.ShowRewardAds(isNormal ? RewardAdsPos.chest_normal_ads : RewardAdsPos.chest_elite_ads, () => {
            Model.ResetFreeOpen();
            OpenChest(false);
        });
    }

    private void OnButtonPriceClicked() {
        ItemStack price = Model.Price;
        ItemStack curPrice = GameResources.Instance.Inventory.GetItem(price.Id);
        if (curPrice.Amount >= price.Amount) {
            OpenChest(true);
            GameResources.Instance.Inventory.Remove(price);
            Tracking.Instance.LogShop(price.Id == ConstantItemID.EliteKey? ShopButton.chest_elite_gem : ShopButton.chest_normal_gem);
        }
        else {
            ShowLockBarNotify(btnPrice.transform);
        }
    }

    private void OnButtonKeyClicked() {
        ItemStack key = Model.KeyOpen;
        GameResources.Instance.Inventory.Remove(key.Id, 1);
        OpenChest(false);
        Tracking.Instance.LogShop(key.Id == ConstantItemID.EliteKey ? ShopButton.chest_elite_key : ShopButton.chest_normal_key);
    }

    private void OpenChest(bool isShowSale) {
        GearSoftData newGear = Model.OpenChest();
        PopupHUD.Instance.OpenChest.SetChest(Model)
                                   .SetGear(newGear)
                                   .SetShowSalePrice(isShowSale);
        PopupHUD.Instance.Show<OpenChestPopup>();
        if (Model.KeyOpen.Id == ConstantItemID.NormalKey) {
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.OpenNormalChest, 1);
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnOpenNormalChest);
        }
        else {
            GameResources.Instance.DailyMission.AddPointProgress(MissionType.OpenEliteChest, 1);
            Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnOpenEliteChest);
        }
    }

    public ChestItemDisplayer SetContentName(string content, bool show) {
        if (txtName) {
            txtName.gameObject.SetActive(show);
            if (show) {
                txtName.text = content;
            }
        }
        return this;
    }
    public ChestItemDisplayer SetContentDesription(string content, bool show) {
        if (txtDescription) {
            txtDescription.gameObject.SetActive(show);
            if (show) {
                txtDescription.text = content;
            }
        }
        return this;
    }
    public ChestItemDisplayer SetIcon(Sprite icon, bool show) {
        if (imgIcon) {
            imgIcon.gameObject.SetActive(show);
            if (show) {
                imgIcon.sprite = icon;
            }
        }
        return this;
    }
    public ChestItemDisplayer SetStateAdsButton(bool interaction, bool show) {
        if (btnAds) {
            btnAds.gameObject.SetActive(show);
            if (show) {
                btnAds.SetState(interaction);
            }
        }
        return this;
    }
    public ChestItemDisplayer SetStatePriceButton(bool interaction, bool show) {
        if (btnPrice) {
            btnPrice.gameObject.SetActive(show);
            if (show) {
                btnPrice.SetState(interaction);
            }
        }
        return this;
    }
    public ChestItemDisplayer SetStateKeyButton(bool interaction, bool show) {
        if (btnKey) {
            btnKey.gameObject.SetActive(show);
            if (show) {
                btnKey.SetState(interaction);
            }
        }
        return this;
    }
    public ChestItemDisplayer SetPriceView(ItemStack item, bool show) {
        if (priceView) {
            priceView.gameObject.SetActive(show);
            if (show) {
                priceView.SetModel(item).Show();
            }
        }
        return this;
    }
    public ChestItemDisplayer SetKeyView(ItemStack item, bool show) {
        if (keyView) {
            keyView.gameObject.SetActive(show);
            if (show) {
                keyView.SetModel(item).Show();
            }
        }
        return this;
    }

    public ChestItemDisplayer SetContentSpecialDescription(string content, bool show) {
        if (txtSpecialDescription) {
            txtSpecialDescription.gameObject.SetActive(show);
            if (show) {
                txtSpecialDescription.text = content;
            }
        }
        return this;
    }

    public ChestItemDisplayer SetContentSpecialDescription(bool show, string gearType) {
        gearSpecialGO.SetActive(show);
        if (show) {
            if (txtSpecialDescription) {
                txtSpecialDescription.gameObject.SetActive(!show);
                txtSpecialGearType.text = gearType;
            }
        }
        return this;
    }

    public ChestItemDisplayer SetContentTimeFree(string content, bool show) {
        if (timeFreeGO) {
            timeFreeGO.SetActive(show);
            if (show) {
                txtTimeFree.text = content;
            }
        }
        return this;
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbar.transform.position = trans.position;
        lockbar.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
}
