
using UnityEngine;

[CreateAssetMenu(fileName = "XmasMissionCompleteTimeData", menuName = "Resource/Missions/MissionItem/Xmas/XmasMissionCompleteTimeData")]
public class XmasMissionCompleteTimeData : XmasMissionItemData {
    public override void Upgrade() {
        int time = IngameHUD.Instance.Combat.GetTime();
        if (time < PointTarget)
            SetProgress(PointTarget);
    }
}
