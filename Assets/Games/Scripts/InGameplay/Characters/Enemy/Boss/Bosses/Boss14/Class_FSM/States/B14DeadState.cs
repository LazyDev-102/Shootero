

using Class_FSM;

public class B14DeadState : B14State {
    #region Singleton
    public B14DeadState() {

    }
    private static B14DeadState instance = null;
    public static B14DeadState Instance {
        get {
            if(instance == null) {
                instance = new B14DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B14Base> controller) {
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return null;
    }
}
