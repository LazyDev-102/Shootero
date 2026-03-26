using DG.Tweening;
using UnityEngine;

public class ZoneProgressPopup : BasePopup {
    [SerializeField] private RewardProgressContainer rewardContainer;
    [SerializeField] private ButtonExplorer claimButton;
    [SerializeField] private Material disableMat;
    [SerializeField] private Material enableMat;

    private int cRewardZone, cRewardWave;
    private System.Action onHide;
    private void Awake() {
        closeButton.AddEvent(OnClose);
        claimButton.AddEvent(OnClaim);
    }
    public void OnHidePopup(System.Action onHide) {
        this.onHide = onHide;
    }
    private void OnEnable() {
        UpdateUI();
    }
    private void UpdateUI(bool moveItem = false) {
        (cRewardWave, cRewardZone) = GameResources.Instance.LevelProgress.Datas.GetCurrentLevelClaimable();
        if (cRewardZone == Constant.ZoneCount && cRewardZone == 25)
            claimButton.SetState(false);
        var uZone = GameResources.Instance.ConquerorData.UnlockZone + 1;
        var uWave = GameResources.Instance.ConquerorData.ZoneDatas[uZone - 1].HighestWave;
        var claimed = GameResources.Instance.LevelProgress.Datas.Rewards.Find(x => x.Zone == cRewardZone && x.Wave == cRewardWave).Claimed;
        DOVirtual.DelayedCall(0.5f, () => {
            claimButton.SetState(!claimed && cRewardZone < uZone || cRewardZone == uZone && cRewardWave <= uWave);
            rewardContainer.SetReachText(!claimButton.interactable);
            PanelHUD.Instance.Conqueror.ZoneProgressNotify();
        });
        rewardContainer.SetReachText(false);
        rewardContainer.Reload(moveItem, SetClaimButtonState);
    }
    private void OnClose() {
        Hide();
        onHide?.Invoke();
    }
    private void OnClaim() {
        claimButton.SetState(false);
        rewardContainer.OnClaimReward(() => UpdateUI(true));
    }
    private void SetClaimButtonState(bool active) {
        claimButton.gameObject.SetActive(active);
    }
}
