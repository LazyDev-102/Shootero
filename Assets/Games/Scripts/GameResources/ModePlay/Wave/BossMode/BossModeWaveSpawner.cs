using DG.Tweening;
using Gemmob;
using Helper;
using UnityEngine;

public class BossModeWaveSpawner : MonoBehaviour {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private BossModeWaveInfo waveInfo;
    protected BossModeController controller;
    private GameLoader gameLoader;
    private ShipBase ship;
    private float currentDifficultMulti;
    private bool isInAttackTime;

    private void OnEnable() {
        EventDispatcher.Instance.AddListener<EventKey.OnBossHpChanged>(CheckDropXp);

    }
    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnBossHpChanged>(CheckDropXp);
    }

    public GameLoader GameLoader {
        get {
            if (gameLoader == null) {
                gameLoader = GameManager.Instance.GameLoader;
            }
            return gameLoader;
        }
    }
    public ShipBase Ship {
        get {
            if (ship == null) {
                ship = GameManager.Instance.GameLoader.Ship;
            }
            return ship;
        }
    }

    public bool IsWinWave {
        get {
            return GameLoader.EnemyCount() == 0;
        }
    }

    public void SetWaveInfo(BossModeWaveInfo waveInfo, float currentDifficultMulti) {
        this.waveInfo = waveInfo;
        this.currentDifficultMulti = currentDifficultMulti;
    }
    public void SetController(BossModeController controller) {
        this.controller = controller;
    }

    public void StartSpawn() {
        SpawnBoss();
        isInAttackTime = true;
    }

    public void SpawnBoss() {
        int bossId = RandomHelper.RandomInCollection(waveInfo.WaveData.BossIds);
        BossBase bossPrefab = GameResources.Instance.EnemyData.GetBossByIndex(bossId - 1);
        BossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss != null) {
            var stat = waveInfo.WaveData.BossModeStats[bossId - 1];
            newBoss.ChangedStatWithMultipler(stat.Atk, stat.Hp, currentDifficultMulti);
            newBoss.Initialize();
            newBoss.RemoveAllOnDie();
            newBoss.AddOnDie(() => {
                if (IngameData.currentGameMode == GameMode.EventBoss) {
                    isInAttackTime = false;
                    this.DelayWait(2, () => {
                        GameManager.Instance.Win();
                    });
                }
            });
            SpawnXpItem();
        }
    }
    private void CheckDropXp(EventKey.OnBossHpChanged param) {
        BossModeDropInfo[] dropInfo = waveInfo.WaveData.DropInfos;
        for (int i = 0; i < dropInfo.Length; i++) {
            if (dropInfo[i].Status)
                continue;
            if (dropInfo[i].Percent > param.Percent) {
                dropInfo[i].Status = true;
                SpawnXpItem();
            }
        }
    }
    private void SpawnXpItem() {
        if (Ship != null) {
            XpDropController xpClone = GameLoader.SpawnDropItem(waveInfo.WaveData.XpDropPrefab, Vector2.up * 15);
            xpClone.transform.localPosition = Vector2.up * 15;
            xpClone.SetValue(Ship.ShipLevel.ExpNeedNextLevel());
        }
    }
}
