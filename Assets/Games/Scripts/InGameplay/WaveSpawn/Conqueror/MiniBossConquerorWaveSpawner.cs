
using Gemmob;
using System.Collections.Generic;
using UnityEngine;

public class MinibossConquerorWaveSpawner : ConquerorWaveSpawner {
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private MinibossConquerorWaveInfo waveInfo;
    private List<int> spawnedMinibosses = new List<int>();


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

    public void SetWaveInfo(MinibossConquerorWaveInfo waveInfo) {
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
        SpawnMiniboss();
    }

    private void SpawnMiniboss() {
        int idRandom;
        do {
            idRandom = waveInfo.GetMinibossId();
        } while (spawnedMinibosses.Contains(idRandom));
        spawnedMinibosses.Add(idRandom);
        MinibossBase bossPrefab = GameResources.Instance.EnemyData.GetMiniBossByIndex(idRandom - 1);
        MinibossBase newBoss = GameLoader.SpawnEnemy(bossPrefab, spawnPosition);
        if (newBoss) {
            newBoss.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * waveInfo.GetWaveMultipler());
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