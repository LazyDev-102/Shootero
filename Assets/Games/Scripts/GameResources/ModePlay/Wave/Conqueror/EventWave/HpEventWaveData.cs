using UnityEngine;



[CreateAssetMenu(fileName = "HpEventWaveData", menuName = "Resource/WaveData/Conqueror/EventWave/Hp")]
public class HpEventWaveData : EventWaveData {
    public override void ChangeEventValue(BasicConquerorWaveInfo waveInfo, float value) {
        waveInfo.HpPercentEvent = value;
    }
}
