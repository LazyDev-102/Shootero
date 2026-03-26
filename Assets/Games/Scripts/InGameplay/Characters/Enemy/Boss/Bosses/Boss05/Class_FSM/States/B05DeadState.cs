

using Class_FSM;

public class B05DeadState : B05State {
    #region Singleton
    public B05DeadState() {

    }
    private static B05DeadState instance = null;
    public static B05DeadState Instance {
        get {
            if(instance == null) {
                instance = new B05DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B05Base> controller) {
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return null;
    }
}
