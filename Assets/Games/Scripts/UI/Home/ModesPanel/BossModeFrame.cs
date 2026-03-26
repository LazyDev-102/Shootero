
using TMPro;
using UnityEngine;

public class BossModeFrame : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI remainTurnText;
    [SerializeField] private ButtonExplorer playButton;
    private BossModeData data;
    private void Awake() {
        playButton.AddEvent(OnPlayGame);
        data = GameResources.Instance.BossModeData;
    }
    private void OnEnable() {
        UpdateUI();
    }
    public void UpdateUI() {
        remainTurnText.text = $"Attemp {data.TurnRemain}/{data.MaxTurn}";
        playButton.SetState(data.TurnRemain > 0);
    }
    private void OnPlayGame() {
        IngameData.PlayGame(GameMode.EventBoss);
    }
}
