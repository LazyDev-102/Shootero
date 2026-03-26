

using Class_FSM;

public class E05DeadState : E05State {
    #region Singleton
    public E05DeadState() {

    }
    private static E05DeadState instance = null;
    public static E05DeadState Instance {
        get {
            if(instance == null) {
                instance = new E05DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E05Base> controller) {
    }

    protected override void DoStartActions(StateController<E05Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E05Base> controller) {
    }

    protected override Transition<E05Base>[] GetTransitions() {
        return null;
    }
}
