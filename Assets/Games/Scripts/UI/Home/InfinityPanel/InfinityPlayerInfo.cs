using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfinityPlayerInfo : MonoBehaviour {
    [SerializeField] private Image iconTop;
    [SerializeField] private TextMeshProUGUI playerRankText;
    [SerializeField] private TextMeshProUGUI playerNameText;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerScoreText;

    private UserProfileInfo data;
    public void Initialize(UserProfileInfo data) {
        this.data = data;
        UpdateUI();
    }
    private void UpdateUI() {
        iconTop.gameObject.SetActive(data.PlayerRank == 1);
        playerRankText.text = $"{data.PlayerRank}";
        playerNameText.text = data.PlayerName;
        playerLevelText.text = $"{data.PlayerLevel}";
        playerScoreText.text = $"{data.PlayerScore}";
    }
    public void UpdateUI(int rank, string name, int lvl, int score) {
        iconTop.gameObject.SetActive(rank == 1);
        playerRankText.text = rank != 0 && rank < 1000 ? $"{rank}" : "...";//$"{data.PlayerRank}";
        playerNameText.text = name;
        playerLevelText.text = $"{lvl}";
        playerScoreText.text = $"{score}";
    }
}
