

using Class_FSM;

public class B09DeadState : B09State {
    #region Singleton
    public B09DeadState() {

    }
    private static B09DeadState instance = null;
    public static B09DeadState Instance {
        get {
            if(instance == null) {
                instance = new B09DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B09Base> controller) {
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return null;
    }
}
