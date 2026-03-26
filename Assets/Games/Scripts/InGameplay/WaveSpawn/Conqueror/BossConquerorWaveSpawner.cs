
using Gemmob;
using UnityEngine;
using System.Collections.Generic;

public class BossConquerorWaveSpawner : ConquerorWaveSpawner {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private BossConquerorWaveInfo waveInfo;
    private List<int> spawnedBosses = new List<int>();

    private void OnEnable() {
        EventDispatcher.Instance.AddListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }

    private void OnDisable() {
        EventDispatcher.Instance.RemoveListener<EventKey.GameStateChangedParam>(OnGameStateChanged);
    }

    private GameLoader gameLoader;
    public GameLoader GameLoader {
        get {
            if (gameLoader == null) {
                gameLoader = GameManager.Instance.GameLoader;
            }
            return gameLoader;
        }
    }

    public void SetWaveInfo(BossConquerorWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }


    public override void EndSpawn() {
    }

    public override bool IsWinWave() {
        return GameLoader.EnemyCount() <= 0;
    }

    public override void OnObjectRemove() {

    }

    public override void StartSpawn() {
        SpawnBoss();
    }

    private void SpawnBoss() {
        int idRandom;
        int loop = 0;
        do {
            idRandom = waveInfo.GetBossId();
            loop++;
            if (loop > 20)
                break;
        } while (spawnedBosses.Contains(idRandom));
        spawnedBosses.Add(idRandom);
        BossBase bossPrefab = GameResources.Instance.EnemyData.GetBossByIndex(idRandom - 1);
        BossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss != null) {
            newBoss.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * controller.CurrentWaveInfo.GetWaveMultipler());
            newBoss.Initialize();
            newBoss.CanDropChip = true;
        }
    }
    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
    }

    public override void OnChangeTypeWave() {
        SoundManager.Instance.StopBackgroundMusic(true, 0.5f, () => {
            SoundManager.Instance.PlayBackgroundBoss(fadein: true, fadeDuration: 0.5f);
        });
    }
}
