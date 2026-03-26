
using UnityEngine;
[CreateAssetMenu(fileName = "MinibossConquerorWaveData", menuName = "Resource/WaveData/Conqueror/Miniboss")]
public class MinibossConquerorWaveData : ConquerorWaveData {
    [SerializeField] private int[] minibossIds;
    [SerializeField] private int chipInIcon = 10;

    public int[] MinibossIds { get => minibossIds; }
    public int ChipInIcon { get => chipInIcon; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        MinibossConquerorWaveInfo waveInfo = new MinibossConquerorWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }
}

