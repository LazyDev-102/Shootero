

using Class_FSM;

public class E17DeadState : E17State {
    #region Singleton
    public E17DeadState() {

    }
    private static E17DeadState instance = null;
    public static E17DeadState Instance {
        get {
            if (instance == null) {
                instance = new E17DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E17Base> controller) {
    }

    protected override void DoStartActions(StateController<E17Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E17Base> controller) {
    }

    protected override Transition<E17Base>[] GetTransitions() {
        return null;
    }
}
