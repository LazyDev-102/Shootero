using Class_FSM;

public class ME01StartState : ME01State {
    #region Singleton
    public ME01StartState() {

    }
    private static ME01StartState instance = null;
    public static ME01StartState Instance {
        get {
            if (instance == null) {
                instance = new ME01StartState();
            }
            return instance;
        }
    }
    #endregion
    private ME01Transition[] transitions = { ME01CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<ME01Base> controller) {
    }

    protected override void DoStartActions(StateController<ME01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<ME01Base> controller) {
    }

    protected override Transition<ME01Base>[] GetTransitions() {
        return transitions;
    }
}
