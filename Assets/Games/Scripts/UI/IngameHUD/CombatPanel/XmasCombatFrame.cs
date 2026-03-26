
using Gemmob;
using TMPro;
using UnityEngine;

public class XmasCombatFrame : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI rewardValueText;

    private int rewardValue = 0;

    private void Start() {
        EventDispatcher.Instance.AddListener(EventKey.OnDropXmasCandy, UpgradeRewardValue);
    }
    private void OnDestroy() {
        EventDispatcher.Instance.RemoveListener(EventKey.OnDropXmasCandy, UpgradeRewardValue);
    }

    public void Active() {
        rewardValue = 0;
        UpdateUI();
    } 
    public void UpdateUI() {
        rewardValueText.text = $"{rewardValue}";
    }

    private void UpgradeRewardValue() {
        rewardValue++;
        UpdateUI();
    }
}
