

using Class_FSM;

public class E14DeadState : E14State {
    #region Singleton
    public E14DeadState() {

    }
    private static E14DeadState instance = null;
    public static E14DeadState Instance {
        get {
            if (instance == null) {
                instance = new E14DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E14Base> controller) {
    }

    protected override void DoStartActions(StateController<E14Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E14Base> controller) {
    }

    protected override Transition<E14Base>[] GetTransitions() {
        return null;
    }
}
