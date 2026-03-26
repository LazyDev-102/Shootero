

public class BossModeWaveInfo {
    private BossModeWaveData waveData;
    private float dropXpDeltaTime;

    public BossModeWaveData WaveData { get => waveData; set => waveData = value; }
    public float DropXpDeltaTime { get => dropXpDeltaTime; }

    public void CreateData(BossModeWaveData waveData) {
        this.WaveData = waveData;
        dropXpDeltaTime = waveData.DropXpDeltaTime.GetRandomValue();
    }
}
