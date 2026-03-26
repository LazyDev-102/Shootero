using UnityEngine;

[CreateAssetMenu(fileName = "SizeEventWaveData", menuName = "Resource/WaveData/Conqueror/EventWave/Size")]
public class SizeEventWaveData : EventWaveData {
    public override void ChangeEventValue(BasicConquerorWaveInfo waveInfo, float value) {
        waveInfo.SizePercentEvent = value;
    }
}
