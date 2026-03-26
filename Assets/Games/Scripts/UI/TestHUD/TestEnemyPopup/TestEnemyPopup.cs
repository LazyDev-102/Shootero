
using GameSystem.Common.UI;
using System;
using UnityEngine;

public class TestEnemyPopup : DOTweenFrame {
    [SerializeField] private ButtonBase btnNormal;
    [SerializeField] private ButtonBase btnElite;
    [SerializeField] private ButtonBase btnChampion;
    [SerializeField] private TestChooseEnemyItemCollectionDisplayer collection;

    EnemyType curType = EnemyType.Normal;


    private void Start() {
        btnNormal.AddEvent(OnSelectNormal);
        btnElite.AddEvent(OnSelectElite);
        btnChampion.AddEvent(OnSelectChampion);

    }

    protected override void OnShow(Action onCompleted = null, bool instant = false) {
        base.OnShow(onCompleted, instant);
        LoadE();
        UpdateTypeButtons();
        GameManager.Instance.Pause();

    }

    private void LoadE() {
#if CHEAT
        EnemyData enemyData = GameResources.Instance.EnemyData;
        int cZone = GameResources.Instance.ConquerorData.CurrentZoneIndex;
        enemyData.PreloadEnemies(cZone, true);
        var eZone = enemyData.ZoneEnemies[cZone].Enemies;
        int length = enemyData.ZoneEnemies[cZone].Enemies.Length;
        EnemyBase[] enemies = new EnemyBase[length];
        for (int i = 0; i < length; ++i) {
            enemies[i] = eZone[i].E[(int)curType];
        }
        collection.AddOnSelect(OnSelect).SetCapacity(enemies.Length).SetItems(enemies).Show();
#endif
    }

    protected override void OnHide(Action onCompleted = null, bool instant = false) {
        base.OnHide(onCompleted, instant);
        GameManager.Instance.Resume();
    }

    private void OnSelect(TestChooseEnemyItemDisplayer displayer) {
        var eClone = GameManager.Instance.GameLoader.SpawnEnemy(displayer.Model, new Vector3(100, 100, 0));
        eClone.Initialize();
        //GameObject.Instantiate(displayer.Model, new Vector3(100, 100, 0), Quaternion.identity, null);
        Hide();
    }


    private void OnSelectNormal() {
        curType = EnemyType.Normal;
        UpdateTypeButtons();
        LoadE();
    }

    private void OnSelectElite() {
        curType = EnemyType.Elite;
        UpdateTypeButtons();
        LoadE();
    }

    private void OnSelectChampion() {
        curType = EnemyType.Champion;
        UpdateTypeButtons();
        LoadE();
    }

    private void UpdateTypeButtons() {

        if (curType == EnemyType.Normal) {
            btnNormal.SetState(false);
            btnElite.SetState(true);
            btnChampion.SetState(true);

        }
        else if (curType == EnemyType.Elite) {
            btnNormal.SetState(true);
            btnElite.SetState(false);
            btnChampion.SetState(true);
        }
        else if (curType == EnemyType.Champion) {
            btnNormal.SetState(true);
            btnElite.SetState(true);
            btnChampion.SetState(false);
        }
    }
}
