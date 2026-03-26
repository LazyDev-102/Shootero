using GameSystem.Common.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearMatrialDetailPopup : DOTweenFrame {
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image icon;
    [SerializeField] private ButtonExplorer closeButton;
    private void Awake() {
        closeButton.AddEvent(OnClose);
    }
    public void UpdateUI(Item data, int quantity) {
        icon.sprite = data.Icon;
        title.text = data.Name;
        descriptionText.text = $"{data.Description}";
        this.quantity.text = quantity.ToString();
        quantityText.text = $"Quantity: {quantity}";
    }
    private void OnClose() {
        Hide();
    }
}
