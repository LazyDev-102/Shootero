
using UnityEngine;
[CreateAssetMenu(fileName = "SupportConquerorWaveData", menuName = "Resource/WaveData/Conqueror/Support")]
public class SupportConquerorWaveData : ConquerorWaveData {
    [SerializeField] private AngelBoss angel;

    public AngelBoss Angel { get => angel; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        SupportConquerorWaveInfo waveInfo = new SupportConquerorWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
}