

using Class_FSM;

public class B10DeadState : B10State {
    #region Singleton
    public B10DeadState() {

    }
    private static B10DeadState instance = null;
    public static B10DeadState Instance {
        get {
            if (instance == null) {
                instance = new B10DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B10Base> controller) {
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return null;
    }
}
