
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenMissionCompleteTimeData", menuName = "Resource/Missions/MissionItem/Halloween/HalloweenMissionCompleteTimeData")]
public class HalloweenMissionCompleteTimeData : HalloweenMissionItemData {
    public override void Upgrade() {
        int time = IngameHUD.Instance.Combat.GetTime();
        if (time < PointTarget)
            SetProgress(PointTarget);
    }
}
