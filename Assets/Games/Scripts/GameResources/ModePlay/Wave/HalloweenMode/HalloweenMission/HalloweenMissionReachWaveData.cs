
using UnityEngine;

[CreateAssetMenu(fileName = "HalloweenMissionReachWaveData", menuName = "Resource/Missions/MissionItem/Halloween/HalloweenMissionReachWaveData")]
public class HalloweenMissionReachWaveData : HalloweenMissionItemData {

    public override void Upgrade() {
        int cWave = GameResources.Instance.Halloween.CurrentWave;
        if (PointProgress < cWave)
            SetProgress(cWave);
    }
}
