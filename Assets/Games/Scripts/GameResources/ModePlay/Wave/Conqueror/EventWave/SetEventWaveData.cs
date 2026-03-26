using UnityEngine;


[CreateAssetMenu(fileName = "Set", menuName = "Resource/WaveData/Conqueror/EventWave/Set")]
public class SetEventWaveData : ScriptableObject {
    [SerializeField] private EventWaveInfo[] eventRules;

    public void ApplyRules(BasicConquerorWaveInfo wave) {
        if (eventRules != null) {
            foreach (var e in eventRules) {
                e.ChangeEventValue(wave);
            }
        }
    }

}
