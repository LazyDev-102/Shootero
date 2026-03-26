

using Class_FSM;

public class B08DeadState : B08State {
    #region Singleton
    public B08DeadState() {

    }
    private static B08DeadState instance = null;
    public static B08DeadState Instance {
        get {
            if (instance == null) {
                instance = new B08DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B08Base> controller) {
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {
    }

    protected override Transition<B08Base>[] GetTransitions() {
        return null;
    }
}
