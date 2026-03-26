using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenMissionItemData", menuName = "Resource/Missions/MissionItem/Halloween/HalloweenMissionItemData")]
public class HalloweenMissionItemData : MissionItemDataBase {
    [SerializeField] private bool descriptionWithTarget;
    [SerializeField] protected HalloweenMissionItemData halloweenCondition;
    [SerializeField] protected HalloweenMissionItemData nextMission;
    [SerializeField] protected bool isLastMission;

    public bool IsLastMission => isLastMission;
    public HalloweenMissionItemData NextMission { get => nextMission;}

    public override bool Active() {
        if (halloweenCondition != null)
            return halloweenCondition.IsComplete && !IsComplete;
        return !IsComplete;
    }

    public string GetDescription() {
        return descriptionWithTarget ? string.Format(MissionDescription, $"{PointProgress}/{PointTarget}") :string.Format(MissionDescription, PointTarget);
    }

    public string GetRewardAmount() {
        return Reward.Length > 0 ? $"{Reward[0].Amount}" : "0";
    }
}
