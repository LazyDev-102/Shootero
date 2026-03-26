using TMPro;
using UnityEngine;

public class ShipEnhanceItem : MonoBehaviour, IItem<ShipSpecialInfo> {
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI discriptionText;
    [SerializeField] private GameObject highLightBar;
    public ShipSpecialInfo dataStack { get; set; }

    public IItem<ShipSpecialInfo> Generate() {

        return this;
    }
    public void UpdateUI(ShipSpecialInfo data, int cLevel, bool highlight) {
        this.dataStack = data;
        levelText.text = $"Lv.{data.Level}";
        discriptionText.text = $"{data.Prefix}{data.GetValue()}{data.Suffix} {data.Description}";
        SetColor(highlight ? Color.cyan : Color.white);
        SetAlpha(cLevel + 1 < data.Level ? 0.3f : 1);
        SetHighLight(highlight);
    }
    public ShipEnhanceItem SetAlpha(float value) {
        discriptionText.SetAlpha(value);
        return this;
    }
    public ShipEnhanceItem SetColor(Color color) {
        discriptionText.SetColor(color);
        return this;
    }
    private void SetHighLight(bool status) {
        if (highLightBar != null) {
            highLightBar.SetActive(status);
        }
    }
}
