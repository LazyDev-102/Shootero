

using Class_FSM;

public class B07DeadState : B07State {
    #region Singleton
    public B07DeadState() {

    }
    private static B07DeadState instance = null;
    public static B07DeadState Instance {
        get {
            if (instance == null) {
                instance = new B07DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B07Base> controller) {
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return null;
    }
}
