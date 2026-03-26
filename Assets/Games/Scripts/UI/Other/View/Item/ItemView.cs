using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemView : View<IItemInstance> {
    [SerializeField] private bool useOldFormat;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private TextMeshProUGUI itemAmount;

    public Image ItemIcon => itemIcon;
    public TextMeshProUGUI ItemName => itemName;
    public TextMeshProUGUI ItemDescription => itemDescription;
    public TextMeshProUGUI ItemAmount => itemAmount;

    public override void Show() {
        if (Model == null) {
            //ItemIcon.SetVisible(false);
            //ItemName.SetVisible(false);
            //ItemDescription.SetVisible(false);
            //ItemAmount.SetVisible(false);
            return;
        }

        if (ItemIcon != null) {
            ItemIcon.sprite = Model.Icon;
        }

        if (ItemName != null) {
            ItemName.text = Model.Name;
        }

        if (ItemDescription != null) {
            ItemDescription.text = Model.Description;
        }

        if (ItemAmount != null) {
            ItemAmount.text = Model.Amount.ToString();
        }

        //if (Model.Amount == int.MaxValue) {
        //    ItemAmount.SetText("∞");
        //}
        //else {
        //    if (useOldFormat) {
        //        ItemAmount.SetText(Model.Amount >= 0 ? Model.Amount.ToString("#,##0") : string.Empty);
        //    }
        //    else {
        //        ItemAmount.SetText(Model.Amount.GetCurrencyString());
        //    }
        //}
    }


    public ItemView SetContentAmount(string content, bool show) {
        if (ItemAmount != null) {
            ItemAmount.gameObject.SetActive(show);
            if (show) {
                ItemAmount.text = content;
            }
        }
        return this;
    }
}
