

using UnityEngine;
[CreateAssetMenu(fileName = "BossConquerorWaveData", menuName = "Resource/WaveData/Conqueror/Boss")]
public class BossConquerorWaveData : ConquerorWaveData {
    [SerializeField] private int[] bossIds;
    [SerializeField] private int chipInIcon = 10;

    public int[] BossIds { get => bossIds; }
    public int ChipInIcon { get => chipInIcon; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        BossConquerorWaveInfo waveInfo = new BossConquerorWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
}
