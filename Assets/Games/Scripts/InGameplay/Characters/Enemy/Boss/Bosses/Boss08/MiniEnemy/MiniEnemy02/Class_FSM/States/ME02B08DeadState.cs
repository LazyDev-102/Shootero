

using Class_FSM;

public class ME02B08DeadState : ME02B08State {
    #region Singleton
    public ME02B08DeadState() {

    }
    private static ME02B08DeadState instance = null;
    public static ME02B08DeadState Instance {
        get {
            if (instance == null) {
                instance = new ME02B08DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<ME02B08Base> controller) {

    }

    protected override void DoStartActions(StateController<ME02B08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<ME02B08Base> controller) {

    }

    protected override Transition<ME02B08Base>[] GetTransitions() {
        return null;
    }
}
