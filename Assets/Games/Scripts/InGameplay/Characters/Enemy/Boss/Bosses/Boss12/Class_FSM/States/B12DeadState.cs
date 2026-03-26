

using Class_FSM;

public class B12DeadState : B12State {
    #region Singleton
    public B12DeadState() {

    }
    private static B12DeadState instance = null;
    public static B12DeadState Instance {
        get {
            if(instance == null) {
                instance = new B12DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B12Base> controller) {
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return null;
    }
}
