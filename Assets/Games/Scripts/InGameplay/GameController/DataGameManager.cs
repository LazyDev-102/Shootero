using Gemmob;
using UnityEngine;

public partial class GameManager {
    [SerializeField] private GameLoader gameLoader;
    [SerializeField] private ControllerAction[] controllerActions;
    private GameController gameController;
    private GameState gameState;
    private GameMode currentGameMode;

    public GameController GameController { get => gameController; }
    public GameLoader GameLoader { get => gameLoader; }
    public GameState GameState { get => gameState; }

    public GameMode GameMode { get => currentGameMode; }
    public ControllerAction[] ControllerActions { get => controllerActions; }

    public T GetGameController<T>() where T : GameController {
        if (gameController is T gc) {
            return gc;
        }
        if (gameController != null)
            Logs.LogError("Get Wrong GameController!!! This is " + gameController.GetType().Name + " not " + typeof(T).Name);
        return null;
    }

    private void SetState(GameState state) {
        gameState = state;
        this.Dispatch(new EventKey.GameStateChangedParam() { gameState = gameState });
    }

    public bool IsState(GameState state) {
        return gameState == state;
    }

    public bool IsStates(GameState[] states) {
        foreach (var state in states) {
            if (IsState(state)) {
                return true;
            }
        }
        return false;
    }
}


public enum GameState {
    None, Playing, Lose, Win, Pause, Revive
}

public enum GameMode {
    Conqueror, Infinity, EventMaterial, EventGear, EventBoss, EventHalloween, EventXmas
}