
using TMPro;
using UnityEngine;

public class GearMaterialItemView : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private ButtonExplorer selectButton;
    private Item data;
    private int value;
    private void Awake() {
        selectButton.AddEvent(OnSelect);
    }
    public void InitData(Item data, int value) {
        this.data = data;
        this.value = value;
        UpdateUI();
    }
    private void UpdateUI() {
        valueText.text = value.ToString();
    }
    private void OnSelect() {
        PopupHUD.Instance.Show<GearMatrialDetailPopup>().UpdateUI(data, value);
    }
}
