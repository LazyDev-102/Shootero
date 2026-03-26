
using UnityEngine;
using UnityEngine.UI;

public class ShipPackItem : MonoBehaviour {
    [SerializeField] private Text pricePack;
    [SerializeField] private Text originPriceText;
    [SerializeField] private ButtonExplorer buyButton;
    [SerializeField] private ButtonExplorer tryButton;
    [SerializeField] private GameAction shipPackAction;

    public ShipPackInfo dataStack { get; set; }

    private void Awake() {
        buyButton.AddEvent(OnBuy);
        tryButton.AddEvent(OnTry);
    }

    public void Initialized(ShipPackInfo data) {
        this.dataStack = data;
        UpdateUI();
    }
    private void UpdateUI() {
        pricePack.text = GameIAP.Instance.GetLocalPrice(dataStack.IapKeySale).localizedPriceString;
        originPriceText.text = GameIAP.Instance.GetLocalPrice(dataStack.IAPKey).localizedPriceString;
        SetStatus(dataStack.Status());
    }
    private void OnBuy() {
        //Tracking.Instance.TrackingIapItemClicked(dataStack.IapKeySale);
        GameIAP.Instance.Buy(dataStack.IapKeySale, OnSuccessBuy, OnBuyFail);

    }
    private void OnSuccessBuy() {
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateClaimUI(dataStack.Rewards).SetTitle("YOU'VE GOT");
        dataStack.Claim(1);
        SetStatus(dataStack.Status());
        //Tracking.Instance.TrackingPurchaseSuccessed(dataStack.IapKeySale);
    }
    private void OnBuyFail() {
        //Tracking.Instance.TrackingPurchaseFaid(dataStack.IapKeySale);
    }
    private void OnTry() {
        shipPackAction.Execute();
        GameResources.Instance.Ship.SetTrial(true, dataStack.ShipId, true);
        IngameData.currentGameMode = GameMode.Conqueror;
        SceneLoader.Instance.LoadSceneAsyn((int)SceneDefined.Index.Tutorial);
    }
    private void SetStatus(bool status) {
        gameObject.SetActive(status);
    }
}
