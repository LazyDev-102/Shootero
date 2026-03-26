
using Gemmob;
using System;
using UnityEngine;

public class BackgroundManager : SingletonBind<BackgroundManager> {
    [Header("InGame")]
    [SerializeField] private SpriteRenderer staticBG;
    [SerializeField] private Transform moveBGContainer;
    [SerializeField] private SpriteRenderer bossBG;
    private MoveBGController moveBGController;

    private void Start() {
        EventDispatcher.Instance.AddListener<EventKey.OnBossSpawnParam>(OnBossSpawn);
        EventDispatcher.Instance.AddListener<EventKey.OnMinibossSpawnParam>(OnMinibossSpawn);

    }

    protected override void OnDestroy() {
        EventDispatcher.Instance.RemoveListener<EventKey.OnBossSpawnParam>(OnBossSpawn);
        EventDispatcher.Instance.RemoveListener<EventKey.OnMinibossSpawnParam>(OnMinibossSpawn);
        base.OnDestroy();
    }

    private void OnBossSpawn(EventKey.OnBossSpawnParam param) {
        bossBG.gameObject.SetActive(param.isSpawn);
        Color bossColor = Color.red;
        if (IngameData.currentGameMode == GameMode.EventHalloween) {
            bossColor = GameResources.Instance.Halloween.EnemyData.GetBossBGColor(param.bossBase);
        }
        else
            if (IngameData.currentGameMode == GameMode.EventXmas) {
            bossColor = GameResources.Instance.Xmas.EnemyData.GetBossBGColor(param.bossBase);
        }
        else
            bossColor = GameResources.Instance.EnemyData.GetBossBGColor(param.bossBase);
        bossColor.a = bossBG.color.a;
        bossBG.color = bossColor;
    }

    private void OnMinibossSpawn(EventKey.OnMinibossSpawnParam param) {
        bossBG.gameObject.SetActive(param.isSpawn);
        Color bossColor = Color.red;
        if (IngameData.currentGameMode == GameMode.EventHalloween) {
            bossColor = GameResources.Instance.Halloween.EnemyData.GetMiniBossBGColor(param.minibossBase);
        }
        else if (IngameData.currentGameMode == GameMode.EventXmas) {
            bossColor = GameResources.Instance.Xmas.EnemyData.GetMiniBossBGColor(param.minibossBase);
        }
        else
            bossColor = GameResources.Instance.EnemyData.GetMiniBossBGColor(param.minibossBase);
        bossColor.a = bossBG.color.a;
        bossBG.color = bossColor;
    }

    public void SetBackground(ZoneBackground data) {
        if (moveBGController)
            Destroy(moveBGController.gameObject);
        staticBG.sprite = data.StaticBG;
        moveBGController = data.MoveBGPrefab.Spawn(moveBGContainer, false);
    }
}

[Serializable]
public class ZoneBackground {
    [SerializeField] private Sprite staticBG;
    [SerializeField] private MoveBGController moveBGPrefab;

    public Sprite StaticBG { get => staticBG; }
    public MoveBGController MoveBGPrefab { get => moveBGPrefab; }
}
