
using TMPro;
using UnityEngine;

public class GearModeFrame : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI remainTurnText;
    [SerializeField] private ButtonExplorer playButton;
    private GearModeData data;
    private void Awake() {
        playButton.AddEvent(OnPlayGame);
        data = GameResources.Instance.GearModeData;
    }
    private void OnEnable() {
        UpdateUI();
    }
    public void UpdateUI() {
        remainTurnText.text = $"Attemp {data.TurnRemain}/{data.MaxTurn}";
        playButton.SetState(data.TurnRemain > 0);
    }
    private void OnPlayGame() {
        IngameData.PlayGame(GameMode.EventGear);
    }
}
