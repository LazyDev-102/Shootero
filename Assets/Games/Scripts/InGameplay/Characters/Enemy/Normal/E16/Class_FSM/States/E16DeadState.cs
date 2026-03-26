

using Class_FSM;

public class E16DeadState : E16State {
    #region Singleton
    public E16DeadState() {

    }
    private static E16DeadState instance = null;
    public static E16DeadState Instance {
        get {
            if (instance == null) {
                instance = new E16DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E16Base> controller) {
    }

    protected override void DoStartActions(StateController<E16Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E16Base> controller) {
    }

    protected override Transition<E16Base>[] GetTransitions() {
        return null;
    }
}
