

using Class_FSM;

public class ME01DeadState : ME01State {
    #region Singleton
    public ME01DeadState() {

    }
    private static ME01DeadState instance = null;
    public static ME01DeadState Instance {
        get {
            if (instance == null) {
                instance = new ME01DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<ME01Base> controller) {
    }

    protected override void DoStartActions(StateController<ME01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<ME01Base> controller) {
    }

    protected override Transition<ME01Base>[] GetTransitions() {
        return null;
    }
}
