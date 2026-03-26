using Gemmob;
using Helper;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialWaveSpawner : ConquerorWaveSpawner {
    private readonly float chooseEnemyDropDeltaTime = 1.0f;
    private readonly Vector3 spawnPosition = new Vector3(100, 100, 0);
    private TutorialWaveInfo waveInfo;
    private bool isStarted;
    private bool isPaused;



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


    public override bool IsWinWave() {
        return hasSpawnE02 && GameLoader.EnemyCount() == 0;
    }

    public void SetWaveInfo(TutorialWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }


    public override void StartSpawn() {
        isStarted = true;
        isPaused = false;
        GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
    }
    public override void EndSpawn() {
        isStarted = false;
    }
    private bool hasSpawnE01;
    private bool hasSpawnE02;
    private void Update() {
        if (!isStarted || isPaused) {
            return;
        }

        if (!hasSpawnE01) {
            hasSpawnE01 = true;
            SpawnEnemies();
            ChooseEnemyDropChip();
        }
        if (!hasSpawnE02 && hasSpawnE01 && GameLoader.EnemyCount() == 0) {
            hasSpawnE02 = true;
            StartCoroutine(SpawnE02());
        }
    }

    private IEnumerator SpawnE02() {
        var popup = IngameHUD.Instance.GetCombat<ConquerorCombatPanel>();
        if (popup != null) {
            popup.HideIntroPlayGame();
            //StartCoroutine(popup.TutorialPanel.ShowIntroPlayGameText(1));
        }

        yield return Yielder.Wait(3f);

        waveInfo.ChangeChipInfor();
        SpawnEnemies(true);
        ChooseEnemyDropChip();
    }
    private void ChooseEnemyDropChip() {
        List<EnemyBase> enemies = GameLoader.Enemies;
        for (int i = 0; i < enemies.Count; ++i) {
            if (!enemies[i].IsDie()/* && e.EnableDropChip && !e.CanDropChip*/) {
                enemies[i].CanDropChip = true;
                //return;
            }
        }
    }

    private void SpawnEnemies(bool isMiniBoss = false) {
        if (isMiniBoss) {
            EnemyBase newEnemy = GameLoader.SpawnEnemy(waveInfo.TutorialWaveData.E02, spawnPosition);
            if (newEnemy) {
                newEnemy.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * controller.CurrentWaveInfo.GetWaveMultipler());
                newEnemy.ChangeStatWithEventValue(1 + waveInfo.AtkPercentEvent, 1 + waveInfo.HpPercentEvent, 1 + waveInfo.SizePercentEvent);
                newEnemy.Initialize();
            }
        }
        else {
            int limitE = waveInfo.Limit;
            for (int i = 0; i < limitE; ++i) {
                EnemyBase newEnemy = GameLoader.SpawnEnemy(waveInfo.TutorialWaveData.E01, spawnPosition);
                if (newEnemy) {
                    newEnemy.ChangedStatWithMultipler(controller.CurrentZoneData.DifficultMultiplier * controller.CurrentWaveInfo.GetWaveMultipler());
                    newEnemy.ChangeStatWithEventValue(1 + waveInfo.AtkPercentEvent, 1 + waveInfo.HpPercentEvent, 1 + waveInfo.SizePercentEvent);
                    newEnemy.Initialize();
                }

            }
        }
    }

    public override void OnObjectRemove() {
    }

    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
        isPaused = param.gameState == GameState.Pause;
    }

    public override void OnChangeTypeWave() {
        SoundManager.Instance.StopBackgroundMusic(true, 0.5f, () => {
            SoundManager.Instance.PlayBackgroundIngame(fadein: true, fadeDuration: 0.5f);
        });
    }
}
