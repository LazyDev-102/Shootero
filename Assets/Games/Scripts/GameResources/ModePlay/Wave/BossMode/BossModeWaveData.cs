using UnityEngine;

[System.Serializable]
public class BossModeWaveData {
    [SerializeField] private int[] bossIds;
    [SerializeField] private RangeFloatValue dropXpDeltaTime;
    [SerializeField] private XpDropController xpDropPrefab;
    [SerializeField] private BossModeDropInfo[] bossModeDropInfos;
    [SerializeField] private BossModeStat[] bossModeStats;

    public int[] BossIds { get => bossIds; set => bossIds = value; }
    public RangeFloatValue DropXpDeltaTime { get => dropXpDeltaTime; }
    public XpDropController XpDropPrefab { get => xpDropPrefab; }
    public BossModeDropInfo[] DropInfos { get => bossModeDropInfos; }
    public BossModeStat[] BossModeStats { get => bossModeStats; }

    public void Preload() {
        GameResources.Instance.EnemyData.PreloadBoss(bossIds, 1);
        for (int i = 0; i < bossModeDropInfos.Length; i++) {
            bossModeDropInfos[i].Status = false;
        }
    }
}
[System.Serializable]
public class BossModeDropInfo {
    public bool Status;
    public float Percent;
}
[System.Serializable]
public class BossModeStat {
    public int BossId;
    public int Hp;
    public int Atk;
}