using UnityEngine;



[CreateAssetMenu(fileName = "LimitEventWaveData", menuName = "Resource/WaveData/Conqueror/EventWave/Limit")]
public class LimitEventWaveData : EventWaveData {
    public override void ChangeEventValue(BasicConquerorWaveInfo waveInfo, float value) {
        waveInfo.LimitPercentEvent = value;
    }
}