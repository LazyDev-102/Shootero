using UnityEngine;
using Helper;

[CreateAssetMenu(fileName = "DailyMissionProgressItemData", menuName = "Resource/HardData/DailyMission/DailyMissionProgressItemData")]
public class DailyMissionProgressItemData : ScriptableObject {
    [SerializeField] private int id;
    [SerializeField] private string progressName;
    [SerializeField] private string progressDescription;
    [SerializeField] private Sprite icon;
    [SerializeField] private int target;
    [SerializeField] private ItemClaim[] rewards;
    [SerializeField] private bool isComplete;

    public int Id { get => id; }
    public string ProgressName { get => progressName; }
    public string ProgressDescription { get => progressDescription; }
    public Sprite Icon { get => icon; }
    public int Target { get => target; }
    public ItemClaim[] Rewards { get => rewards; }
    public bool IsComplete { get => isComplete; }
    public void Assign() {
        RefreshReward();
    }
    public void SetIsComplete(int value) {
        isComplete = value >= target;
    }
    private void RefreshReward() {
        if (rewards == null || rewards.Length == 0)
            return;
        foreach (var item in rewards) {
            if (item.Id == ConstantItemID.RandomMatId) {
                var value = (GameResources.Instance.MaterialPerSecond * Constant.HourToSecond).ConvertToInt();
                if (value < 1)
                    value = 1;
                item.Amount = value;
            }
        }
    }
    public virtual void Claim(int amount = 1) {
        if (rewards == null)
            return;
        foreach (var item in rewards) {
            if (item != null)
                item.Claim(amount);
        }
        isComplete = true;
    }
    public virtual bool Claimable(int progress) {
        if (IsComplete)
            return false;
        if (progress < target)
            return false;
        return true;
    }
    public virtual void ResetData() {
        isComplete = false;
    }
}
