

using Class_FSM;

public class B15DeadState : B15State {
    #region Singleton
    public B15DeadState() {

    }
    private static B15DeadState instance = null;
    public static B15DeadState Instance {
        get {
            if(instance == null) {
                instance = new B15DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B15Base> controller) {
    }

    protected override void DoStartActions(StateController<B15Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return null;
    }
}
