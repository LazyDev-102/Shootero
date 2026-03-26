

using Class_FSM;

public class B13DeadState : B13State {
    #region Singleton
    public B13DeadState() {

    }
    private static B13DeadState instance = null;
    public static B13DeadState Instance {
        get {
            if (instance == null) {
                instance = new B13DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B13Base> controller) {
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return null;
    }
}
