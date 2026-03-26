using UnityEngine;

[CreateAssetMenu(fileName = "TutorialWaveData", menuName = "Resource/WaveData/Conqueror/Tutorial")]
public class TutorialWaveData : ConquerorWaveData { // hardData
    [SerializeField] private int[] enemyIds;
    [SerializeField] private EnemyBase e01;
    [SerializeField] private EnemyBase e02;
    [SerializeField] private int numberE01;


    public int[] EnemyIds { get => enemyIds; }
    public int NumberE01 { get => numberE01; }
    public EnemyBase E02 { get => e02; }
    public EnemyBase E01 { get => e01; }

    public override ConquerorWaveInfo CreateInfo(int currentZoneIndex, int currentWaveIndex) {
        TutorialWaveInfo waveInfo = new TutorialWaveInfo();
        waveInfo.CreateData(currentZoneIndex, currentWaveIndex, this);
        return waveInfo;
    }


}