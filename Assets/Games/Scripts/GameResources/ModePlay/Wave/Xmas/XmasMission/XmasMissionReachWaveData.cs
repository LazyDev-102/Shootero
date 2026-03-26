
using UnityEngine;

[CreateAssetMenu(fileName = "XmasMissionReachWaveData", menuName = "Resource/Missions/MissionItem/Xmas/XmasMissionReachWaveData")]
public class XmasMissionReachWaveData : XmasMissionItemData {

    public override void Upgrade() {
        int cWave = GameResources.Instance.Xmas.CurrentWave;
        if (PointProgress < cWave)
            SetProgress(cWave);
    }
}
