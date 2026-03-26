using Gear_Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearDetailItemStatsSecondary : MonoBehaviour, IItem<RankStat> {
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI valueIncreaseText;
    [SerializeField] GameObject arrow;
    [SerializeField] GameObject newItem;
    [SerializeField] GameObject dotItem;
    [SerializeField] Image background;

    public RankStat dataStack { get; set; }
    private int currentLevel;
    private bool isUpgradePanel;

    public IItem<RankStat> Generate() {
        if (dotItem != null)
            dotItem.SetActive(true);
        if (newItem != null) {
            newItem.SetActive(false);
        }
        if (arrow != null)
            arrow.SetActive(true);
        mainText.color = Color.white;
        if (isUpgradePanel) {
            if (currentLevel == 0) {
                mainText.text = dataStack.StatData.GetDescription(dataStack.Values[currentLevel].Value);
            }
            else {
                mainText.text = dataStack.StatData.GetDescription(dataStack.Values[currentLevel - 1].Value);
                valueIncreaseText.text = $" {dataStack.StatData.GetValueString(dataStack.Values[currentLevel].Value)}";
            }
        }
        else {
            mainText.text = dataStack.StatData.GetDescription(dataStack.Values[currentLevel].Value);
        }
        return this;
    }

    public void Initialized(RankStat data, int currentLevel, bool isUpgradePanel) {
        this.dataStack = data;
        this.currentLevel = currentLevel;
        this.isUpgradePanel = isUpgradePanel;
        Generate();
    }

    public void InitializedSpecial() {
        if (dotItem != null)
            dotItem.SetActive(false);
        if (newItem != null) {
            newItem.SetActive(true);
            mainText.color = Color.yellow;
            mainText.text = dataStack.StatData.GetDescription(dataStack.Values[currentLevel].Value);
        }
        if (arrow != null)
            arrow.SetActive(false);
    }
    public void ActiceBackground(bool active) {
        if (background != null)
            background.enabled = active;
    }
}
