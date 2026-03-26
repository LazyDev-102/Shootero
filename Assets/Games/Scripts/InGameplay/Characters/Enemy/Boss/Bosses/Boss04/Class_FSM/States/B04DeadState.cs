

using Class_FSM;

public class B04DeadState : B04State {
    #region Singleton
    public B04DeadState() {

    }
    private static B04DeadState instance = null;
    public static B04DeadState Instance {
        get {
            if (instance == null) {
                instance = new B04DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B04Base> controller) {

    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return null;
    }
}
