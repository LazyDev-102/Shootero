

using Class_FSM;

public class ME03B08DeadState : ME03B08State {
    #region Singleton
    public ME03B08DeadState() {

    }
    private static ME03B08DeadState instance = null;
    public static ME03B08DeadState Instance {
        get {
            if (instance == null) {
                instance = new ME03B08DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<ME03B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<ME03B08Base> controller) {
    }

    protected override Transition<ME03B08Base>[] GetTransitions() {
        return null;
    }
}
