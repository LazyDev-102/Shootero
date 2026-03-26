using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyFreeItemDisplayer : View<DailyFreePackItem> {
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private Image imgIcon;

    public override void Show() {
        if (Model == null) {
            return;
        }
        txtAmount.text = $"{Model.ItemClaims[0].Amount}";
        imgIcon.sprite = Model.Icon;
    }
    public void Claim(int multi) {
        Model.Claim(multi);
        GameResources.Instance.DailyMission.AddPointProgress(MissionType.ClaimDailyFreePack, 1);
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnClaimDailyFree);
        Tracking.Instance.LogShop(ShopButton.daily_free_pack);
    }
}
