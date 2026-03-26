
using GameSystem.Common.UI;
using System;
using UnityEngine;

public class TestMiniBossPopup : DOTweenFrame {
    [SerializeField] private TestChooseMiniBossItemCollectionDisplayer collection;


    private MinibossBase curMiniBossbase;

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        MinibossPrefabData.MinibossData[] bossData = GameResources.Instance.EnemyData.MinibossDatas.Minibosses;
        MinibossBase[] bossbase = new MinibossBase[bossData.Length];
        for (int i = 0; i < bossbase.Length; ++i) {
            bossbase[i] = bossData[i].minibossBase;
        }
        collection.AddOnSelect(OnSelect).SetCapacity(bossbase.Length).SetItems(bossbase).Show();
        GameManager.Instance.Pause();
    }

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        GameManager.Instance.Resume();
    }

    private void OnSelect(TestChooseMiniBossItemDisplayer displayer) {
        if (curMiniBossbase) {
            curMiniBossbase.SelfDestruction();
        }

        var m = GameManager.Instance.GameLoader.SpawnEnemy(displayer.Model, new Vector3(100, 100, 0));
        m.Initialize();
        //curMiniBossbase = GameObject.Instantiate(displayer.Model, new Vector3(100, 100, 0), Quaternion.identity, null);
        Hide();
    }
}
