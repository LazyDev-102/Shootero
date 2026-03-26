using UnityEngine;


[CreateAssetMenu(fileName = "AtkEventWaveData", menuName = "Resource/WaveData/Conqueror/EventWave/Atk")]
public class AtkEventWaveData : EventWaveData {
    public override void ChangeEventValue(BasicConquerorWaveInfo waveInfo, float value) {
        waveInfo.AtkPercentEvent = value;
    }
}
