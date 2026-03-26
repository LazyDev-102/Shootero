
using TMPro;
using UnityEngine;

public class MaterialModeFrame : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI remainTurnText;
    [SerializeField] private ButtonExplorer playButton;
    private MaterialModeData data;
    private void Awake() {
        playButton.AddEvent(OnPlayGame);
        data = GameResources.Instance.MaterialModeData;
    }
    private void OnEnable() {
        UpdateUI();
    }
    public void UpdateUI() {
        remainTurnText.text = $"Attemp {data.TurnRemain}/{data.MaxTurn}";
        playButton.SetState(data.TurnRemain > 0);
    }
    private void OnPlayGame() {
        GameResources.Instance.Inventory.EnoughPrice(data.EnergyNeed, () => {
            IngameData.PlayGame(GameMode.EventMaterial);
        }, () => {
            PopupHUD.Instance.Show<MoreEnergyPopup>();
        });
    }
}
