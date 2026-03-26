
[System.Serializable]
public abstract class ControllerAction : GameAction<GameManager> {
    public virtual GameController GetController(GameManager manager) {
        return new ConquerorController(manager);
    }
}
