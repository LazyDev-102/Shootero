


public static class IngameData {
    public static int currentZoneIndex;
    public static GameMode currentGameMode = GameMode.Conqueror;

    public static void PlayGame(GameMode gameMode, System.Action onFadein = null) {
        currentGameMode = gameMode;
        SceneLoader.Instance.LoadGamePlayScene(LoadSceneType.LoadAsyn, onFadeIn: onFadein);
    }
}

