using System;
using UnityEngine;

public abstract class EventWaveData : ScriptableObject {
    public abstract void ChangeEventValue(BasicConquerorWaveInfo waveInfo, float value);

}


[Serializable]
public class EventWaveInfo {
    [SerializeField] private EventWaveData eventData;
    [SerializeField] private float value;

    public void ChangeEventValue(BasicConquerorWaveInfo waveInfo) {
        if (eventData) {
            eventData.ChangeEventValue(waveInfo, value);
        }
    }
}
