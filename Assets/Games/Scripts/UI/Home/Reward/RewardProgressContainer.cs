using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RewardProgressContainer : ContainerBase<ItemStack> {
    [SerializeField] private List<ProgressRewardUI> items;
    [SerializeField] private TextMeshProUGUI reachText;
    [HideInInspector] public int CurrentLevelTarget;

    [SerializeField] private ProgressRewardUI itemVirtualMid;
    [SerializeField] private ProgressRewardUI itemVirtualRight;
    [SerializeField] private TextMeshProUGUI menuRewardText;


    private List<RewardProgressItem> rewardItems = new List<RewardProgressItem>();
    private LevelProgressPref data;
    private int preLevel, cLevel, nextLevel, preZone, cZone, nextZone;

    protected override IEnumerable<ItemStack> GetData() {
        data = GameResources.Instance.LevelProgress.Datas;
        CurrentLevelTarget = data.GetMinLevelClaimable();
        var items = data.Rewards[CurrentLevelTarget].ItemRewards;
        foreach (var item in items) {
            yield return item;
        }
    }
    protected override void OnEnable() {

    }
    private void UpdateUI() {
        (preLevel, preZone) = data.GetPreLevelClaimable();
        (cLevel, cZone) = data.GetCurrentLevelClaimable();
        (nextLevel, nextZone) = data.GetNextLevelClaimable();
        items[0].UpdateUI(preZone, preLevel);
        items[1].UpdateUI(cZone, cLevel).SetFrameSelect(!data.Rewards[CurrentLevelTarget].Claimed);
        items[2].UpdateUI(nextZone, nextLevel);

        itemVirtualMid.UpdateUI(cZone, cLevel);
        itemVirtualRight.UpdateUI(nextZone, nextLevel);
        items[2].gameObject.SetActive(true);
        items[2].Fade(0.2f);
    }
    public void Reload(bool moveItem, System.Action<bool> onOverData) {
        Generate();
        rewardItems.Clear();
        foreach (var item in Items) {
            rewardItems.Add(item as RewardProgressItem);
        }
        if (data.Rewards[CurrentLevelTarget].ComingSoon) {
            menuRewardText.text = "Coming Soon";
            foreach (var item in rewardItems) {
                item.gameObject.SetActive(false);
            }
            onOverData.Invoke(false);
        }
        if (moveItem)
            MoveItem(UpdateUI);
        else
            UpdateUI();
    }

    private void MoveItem(System.Action onComplete) {
        items[0].gameObject.SetActive(false);
        items[1].gameObject.SetActive(false);
        items[2].gameObject.SetActive(false);
        itemVirtualMid.gameObject.SetActive(true);
        itemVirtualRight.gameObject.SetActive(true);

        itemVirtualMid.MoveToTarget(items[0].transform, 0.1f, null);
        itemVirtualRight.MoveToTarget(items[1].transform, 0.2f, () => {
            itemVirtualMid.gameObject.SetActive(false);
            itemVirtualRight.gameObject.SetActive(false);
            items[0].gameObject.SetActive(true);
            items[1].gameObject.SetActive(true);
            onComplete?.Invoke();
        });
    }

    public void OnClaimReward(System.Action onComplete) {
        if (CurrentLevelTarget == -1)
            return;
        GameResources.Instance.Inventory.Add(data.Rewards[CurrentLevelTarget].ItemRewards);
        data.Rewards[CurrentLevelTarget].SetClaim(true);
        ShowReward(onComplete);
    }
    private void ShowReward(System.Action onComplete) {
        //PopupHUD.Instance.ZoneProgress.Pause();
        PopupHUD.Instance.Show<RewardPopup>(hideCurrent: false).UpdateUI(data.Rewards[CurrentLevelTarget].ItemRewards, onClose: () => {
            //PopupHUD.Instance.ZoneProgress.Resume();
            onComplete?.Invoke();
        });
    }
    public void SetReachText(bool active) {
        if (active) {
            data = GameResources.Instance.LevelProgress.Datas;
            (int newCLevel, int newCZone) = data.GetCurrentLevelClaimable();
            reachText.text = $"Reach Zone {newCZone}, Wave {newCLevel}";
        }
        reachText.gameObject.SetActive(active);
    }
}
