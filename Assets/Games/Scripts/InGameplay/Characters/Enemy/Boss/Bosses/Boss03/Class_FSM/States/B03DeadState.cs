

using Class_FSM;

public class B03DeadState : B03State {
    #region Singleton
    public B03DeadState() {

    }
    private static B03DeadState instance = null;
    public static B03DeadState Instance {
        get {
            if(instance == null) {
                instance = new B03DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B03Base> controller) {
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return null;
    }
}
