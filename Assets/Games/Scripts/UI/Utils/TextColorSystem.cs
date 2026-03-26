using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextColorSystem : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color notEnoughColor = Color.red;
    [SerializeField] private Color highlightColor = Color.yellow;
    [SerializeField] private CurrencyType currencyType = CurrencyType.Chip;
    [SerializeField] private bool useHighlight;
    private int result = 0;

    private void SetColor() {
        switch (currencyType) {
            case CurrencyType.Chip:
                if (result > GameResources.Instance.Inventory.GetItem(ConstantItemID.ChipId).Amount)
                    valueText.SetColor(notEnoughColor);
                else
                    valueText.SetColor(useHighlight ? highlightColor : normalColor);
                break;
            case CurrencyType.Gem:
                if (result > GameResources.Instance.Inventory.GetItem(ConstantItemID.GemId).Amount)
                    valueText.SetColor(notEnoughColor);
                else
                    valueText.SetColor(useHighlight ? highlightColor : normalColor);
                break;
            case CurrencyType.Energy:
                if (result > GameResources.Instance.Inventory.GetItem(ConstantItemID.EnergyId).Amount)
                    valueText.SetColor(notEnoughColor);
                else
                    valueText.SetColor(useHighlight ? highlightColor : normalColor);
                break;
        }
    }
    public void SetData(int value, CurrencyType type) {
        gameObject.SetActive(true);
        valueText.gameObject.SetActive(true);
        result = value;
        valueText.text = value.ToString();
        currencyType = type;
        SetColor();
    }
    public void SetValueText(string value) {
        valueText.text = value;
    }
}
