

public interface IActionGameControllable {
    void RemoveEnemy(EnemyInfo eInfo);
    void CheckWinWave();
    void AddScore(int score);

    void EndSeasonGame();
    void QuitGame();
}
