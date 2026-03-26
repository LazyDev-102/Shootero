using Gear_Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearDetailItemStat : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI currentValueText;
    [SerializeField] private TextMeshProUGUI nextValueText;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Image background;

    private RankStat data;
    private float alpha;
    public GearDetailItemStat UpdateUI(RankStat data, int rank, bool isMaxRank, bool isNew, float alpha = 1f) {
        SetData(data, alpha);
        if (data != null) {
            var nextRank = rank >= data.Values.Length - 1 ? rank : rank + 1;
            SetCurrenValueText(data.StatData.GetDescription(data.Values[rank].Value), isNew ? Color.green : Color.white);
            SetNextValueText(/*isMaxRank || */isNew ? "" : data.StatData.GetValueString(data.Values[nextRank].Value));
            SetNewItemStatus(isNew);
        }
        else {
            SetCurrenValueText("???", Color.white);
            SetNextValueText("");
            SetNewItemStatus(false);
        }
        return this;
    }

    public GearDetailItemStat SetData(RankStat data, float alpha) {
        this.data = data;
        this.alpha = alpha;
        return this;
    }
    public GearDetailItemStat SetCurrenValueText(string value, Color color) {
        currentValueText.text = value;
        currentValueText.color = color;
        currentValueText.SetAlpha(alpha);
        return this;
    }
    public GearDetailItemStat SetNextValueText(string value) {
        nextValueText.text = value;
        nextValueText.SetAlpha(alpha);
        return this;
    }
    public GearDetailItemStat SetArrowStatus(bool status) {
        if (arrow != null)
            arrow.SetActive(status);
        return this;
    }
    public GearDetailItemStat SetNewItemStatus(bool status) {
        if (background != null)
            background.gameObject.SetActive(status);
        return this;
    }
}
