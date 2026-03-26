

using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RerollPackItemDisplayer : View<RerollPackItem> {
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private TextMeshProUGUI watchLeftText;
    [SerializeField] private TextMeshProUGUI timeRemainText;
    [SerializeField] private Image imgIcon;
    [SerializeField] private ButtonBase btnBuy;
    [SerializeField] private ButtonExplorer watchButton;
    [SerializeField] private ItemView priceView;
    [SerializeField] private GameObject remainGroup;
    [SerializeField] private GameObject tagFree;
    [SerializeField] private LockbarNotify lockbarNotify;

    private bool isFree;
    private bool isWait;
    private double timeRemain = 0;
    private double currentTime = 0;
    private TimeSpan timeSpan;
    private Countdowner showTimeRemainTextCD = new Countdowner();
    private void Start() {
        btnBuy?.AddEvent(OnButtonBuyClicked);
        watchButton?.AddEvent(OnWatchVideo);
    }

    public override void Show() {
        if (Model == null) {
            return;
        }
        isFree = Model.IsFree;
        UpdateUI();
    }
    private void Update() {
        if (isWait)
            SetTimeRemain();

    }
    private void OnButtonBuyClicked() {
        ItemStack price = Model.Price;
        ItemStack curItem = GameResources.Instance.Inventory.GetItem(price.Id);
        if (curItem.Amount >= price.Amount) {
            GameResources.Instance.Inventory.Remove(price);
            Model.Claim(1);
            UpdateUI();
            Tracking.Instance.LogShop(Model.ShopButtonKey);
        }
        else {
            ShowLockBarNotify(btnBuy.transform);
        }
    }
    private void OnWatchVideo() {
        EMAdManager.Instance.ShowRewardAds(RewardAdsPos.reroll_ads, () => {
            Model.Claim(1);
            UpdateUI();
        });
    }
    private void UpdateUI() {
        txtName.text = Model.Name;
        watchLeftText.text = $"1/Day";
        txtAmount.text = $"<color=yellow>{Model.ItemClaims[0].Amount}</color>";
        imgIcon.sprite = Model.Icon;
        tagFree.SetActive(isFree && !Model.Watched);
        priceView.SetModel(Model.Price).Show();
        isWait = isFree && Model.Watched;
        SetButtonStatus();
    }
    private void SetButtonStatus() {
        priceView.gameObject.SetActive(!isFree);
        btnBuy.SetState(!isFree);
        watchButton.gameObject.SetActive(isFree && !Model.Watched);
        watchButton.SetState(isFree && !Model.Watched);
        remainGroup.SetActive(isFree && Model.Watched);
        lockbarNotify.gameObject.SetActive(false);
    }
    private void SetTimeRemain() {
        if (showTimeRemainTextCD.IsTimeOut()) {
            currentTime = DateTime.Now.TimeOfDay.TotalSeconds;
            timeRemain = Constant.DayToSecond - currentTime;
            timeSpan = TimeSpan.FromSeconds(timeRemain);
            timeRemainText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            if (timeRemain <= 0) {
                isWait = false;
                SetButtonStatus();
                GameResources.Instance.ShopData.Rerolls.Resetable();
            }
            showTimeRemainTextCD.StartCountdown(1);
        }
        else {
            showTimeRemainTextCD.Countdowning(Time.deltaTime);
        }
    }
    public void ShowLockBarNotify(Transform trans) {
        lockbarNotify.transform.position = trans.position;
        lockbarNotify.SetOriginPos(trans.position - Vector3.up * 1).SetContent(GameDefine.InsufficientResources, 0.5f).Show();
    }
}
