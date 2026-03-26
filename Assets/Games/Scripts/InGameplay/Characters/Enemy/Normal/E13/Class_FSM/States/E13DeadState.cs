

using Class_FSM;

public class E13DeadState : E13State {
    #region Singleton
    public E13DeadState() {

    }
    private static E13DeadState instance = null;
    public static E13DeadState Instance {
        get {
            if (instance == null) {
                instance = new E13DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E13Base> controller) {
    }

    protected override void DoStartActions(StateController<E13Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E13Base> controller) {
    }

    protected override Transition<E13Base>[] GetTransitions() {
        return null;
    }
}
