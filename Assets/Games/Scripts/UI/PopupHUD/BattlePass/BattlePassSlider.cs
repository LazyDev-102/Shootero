using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattlePassSlider : MonoBehaviour {
    [SerializeField] private Image progressImage;
    [SerializeField] private Image mainIcon;
    [SerializeField] private SpreadEffectUI spreadEffect;
    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI nextLevel;
    [SerializeField] private TextMeshProUGUI seasonText;
    [SerializeField] private GameObject nextBackground;
    [SerializeField] private GameObject notice;

    private BattlePassData data;
    private void Awake() {
        data = GameResources.Instance.BattlePass;
    }
    private void OnEnable() {
        UpdateUI();
    }
    public void UpdateUI() {
        spreadEffect.UpdateUI(data.Claimable());
        seasonText.text = $"Season {data.SeasonIndex}";
        level.text = $"{data.Progress}";
        nextLevel.text = $"{data.Progress + 1}";
        progressImage.fillAmount = data.Ratio();
        nextLevel.gameObject.SetActive(data.Progress < data.Count);
        nextBackground.gameObject.SetActive(data.Progress < data.Count);
    }
}
