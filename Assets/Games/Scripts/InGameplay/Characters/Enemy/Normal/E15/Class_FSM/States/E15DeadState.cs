

using Class_FSM;

public class E15DeadState : E15State {
    #region Singleton
    public E15DeadState() {

    }
    private static E15DeadState instance = null;
    public static E15DeadState Instance {
        get {
            if (instance == null) {
                instance = new E15DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E15Base> controller) {
    }

    protected override void DoStartActions(StateController<E15Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E15Base> controller) {
    }

    protected override Transition<E15Base>[] GetTransitions() {
        return null;
    }
}
