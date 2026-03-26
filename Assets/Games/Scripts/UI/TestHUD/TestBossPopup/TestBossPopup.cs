
using GameSystem.Common.UI;
using System;
using UnityEngine;

public class TestBossPopup : DOTweenFrame {
    [SerializeField] private TestChooseBossItemCollectionDisplayer collection;


    private BossBase curBossbase;

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        BossPrefabData.BossData[] bossData = GameResources.Instance.EnemyData.BossDatas.Bosses;
        BossBase[] bossbase = new BossBase[bossData.Length];
        for (int i = 0; i < bossbase.Length; ++i) {
            bossbase[i] = bossData[i].bossBase;
        }
        collection.AddOnSelect(OnSelect).SetCapacity(bossbase.Length).SetItems(bossbase).Show();
        GameManager.Instance.Pause();
    }

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        GameManager.Instance.Resume();
    }

    private void OnSelect(TestChooseBossItemDisplayer displayer) {
        if (curBossbase) {
            curBossbase.SelfDestruction();
        }

        var boss = GameManager.Instance.GameLoader.SpawnEnemy(displayer.Model, new Vector3(100, 100, 0));
        boss.Initialize();
        //curBossbase = GameObject.Instantiate(displayer.Model, new Vector3(100, 100, 0), Quaternion.identity, null);
        Hide();
    }
}
