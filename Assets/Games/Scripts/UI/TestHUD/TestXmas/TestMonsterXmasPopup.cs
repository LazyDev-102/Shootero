using GameSystem.Common.UI;
using UnityEngine;

public class TestMonsterXmasPopup : DOTweenFrame {
    [SerializeField] ButtonBase xe01Button;
    [SerializeField] ButtonBase xe02Button;
    [SerializeField] ButtonBase xe03Button;
    [SerializeField] ButtonBase xe04Button;
    [SerializeField] ButtonBase xmb01Button;
    [SerializeField] ButtonBase xmb02Button;
    [SerializeField] ButtonBase xb01Button;

    private void Start() {
        xe01Button.AddEvent(SpawnXE01);
        xe02Button.AddEvent(SpawnXE02);
        xe03Button.AddEvent(SpawnXE03);
        xe04Button.AddEvent(SpawnXE04);
        xmb01Button.AddEvent(SpawnXMB01);
        xmb02Button.AddEvent(SpawnXMB02);
        xb01Button.AddEvent(SpawnXB01);
    }

    private void SpawnXE01() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetEnemyBaseRandom(new int[] { 1 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnXE02() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetEnemyBaseRandom(new int[] { 2 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnXE03() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetEnemyBaseRandom(new int[] { 3 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnXE04() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetEnemyBaseRandom(new int[] { 4 }, EnemyType.Champion), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnXMB01() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetMiniBossByIndex(0), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnXMB02() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetMiniBossByIndex(1), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
    private void SpawnXB01() {
        var enemy = GameManager.Instance.GameLoader.SpawnEnemy(GameResources.Instance.Xmas.Prefab.GetBossByIndex(0), new Vector3(50, 50, 0));
        enemy.Initialize();
        Hide();
    }
}
