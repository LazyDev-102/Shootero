using GameSystem.Common.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonsterHalloweenPopup : DOTweenFrame {
    [SerializeField] ButtonBase he01Button;
    [SerializeField] ButtonBase he02Button;
    [SerializeField] ButtonBase he03Button;
    [SerializeField] ButtonBase he04Button;
    [SerializeField] ButtonBase hmb01Button;
    [SerializeField] ButtonBase hmb02Button;
    [SerializeField] ButtonBase hb01Button;

    private void Start() {
        he01Button.AddEvent(SpawnHE01);
        he02Button.AddEvent(SpawnHE02);
        he03Button.AddEvent(SpawnHE03);
        he04Button.AddEvent(SpawnHE04);
        hmb01Button.AddEvent(SpawnHMB01);
        hmb02Button.AddEvent(SpawnHMB02);
        hb01Button.AddEvent(SpawnHB01);
    }

    private void SpawnHE01() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetEnemyBaseRandom(new int[] { 1 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnHE02() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetEnemyBaseRandom(new int[] { 2 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnHE03() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetEnemyBaseRandom(new int[] { 3 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnHE04() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetEnemyBaseRandom(new int[] { 4 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnHMB01() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetMiniBossByIndex(0), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnHMB02() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetMiniBossByIndex(1), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnHB01() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Halloween.Prefab.GetBossByIndex(0), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
}
