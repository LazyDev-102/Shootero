using DG.Tweening;
using UnityEngine;

public class RewardAdsPogressItemDisplayer : MonoBehaviour {
    [SerializeField] ButtonBase claimButton;
    [SerializeField] GameObject lockBg;
    [SerializeField] GameObject lockLevel;
    [SerializeField] ShopRewardAdsProgressItem data;
    [SerializeField] DOTweenAnimation claimableTween;


    private void Start() {
        claimButton.AddEvent(OnClaimReward);
    }

    public void UpdateUI() {
        bool claimable = data.Claimable();
        claimButton.SetState(claimable);
        lockBg.SetActive(data.Claimed);
        lockLevel.SetActive(data.Claimed);
        if (claimable)
            claimableTween.DOPlay();
        else
            claimableTween.DOPause();
    }

    private void OnClaimReward() {
        data.Claim(1);
        UpdateUI();
    }
}
