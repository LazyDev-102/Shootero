using UnityEngine;

[CreateAssetMenu(fileName = "XmasMissionItemData", menuName = "Resource/Missions/MissionItem/Xmas/XmasMissionItemData")]
public class XmasMissionItemData : MissionItemDataBase {
    [SerializeField] private bool descriptionWithTarget;
    [SerializeField] protected XmasMissionItemData XmasCondition;
    [SerializeField] protected XmasMissionItemData nextMission;
    [SerializeField] protected bool isLastMission;

    public bool IsLastMission => isLastMission;
    public XmasMissionItemData NextMission { get => nextMission;}

    public override bool Active() {
        if (XmasCondition != null)
            return XmasCondition.IsComplete && !IsComplete;
        return !IsComplete;
    }

    public string GetDescription() {
        return descriptionWithTarget ? string.Format(MissionDescription, $"{PointProgress}/{PointTarget}") :string.Format(MissionDescription, PointTarget);
    }

    public string GetRewardAmount() {
        return Reward.Length > 0 ? $"{Reward[0].Amount}" : "0";
    }
}
