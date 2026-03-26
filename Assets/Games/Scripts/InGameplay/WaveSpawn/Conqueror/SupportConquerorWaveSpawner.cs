
using Gemmob;
using UnityEngine;

public class SupportConquerorWaveSpawner : ConquerorWaveSpawner {
    private SupportConquerorWaveInfo waveInfo;
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

    public void SetWaveInfo(SupportConquerorWaveInfo waveInfo) {
        this.waveInfo = waveInfo;
    }

    public override void EndSpawn() {
    }

    public override bool IsWinWave() {
        return false;
    }

    public override void OnObjectRemove() {

    }

    public override void StartSpawn() {
        var angelClone = waveInfo.SupportWaveData.Angel.Spawn(GameManager.Instance.GameLoader.transform);
        angelClone.Init();
    }

    private void OnGameStateChanged(EventKey.GameStateChangedParam param) {
    }

    public override void OnChangeTypeWave() {
        SoundManager.Instance.StopBackgroundMusic(true, 0.5f, () => {
            SoundManager.Instance.PlayBackgroundSupport(fadein: true, fadeDuration: 0.5f);
        });
    }
}