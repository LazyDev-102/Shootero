

using Class_FSM;

public class E03DeadState : E03State {
    #region Singleton
    public E03DeadState() {

    }
    private static E03DeadState instance = null;
    public static E03DeadState Instance {
        get {
            if(instance == null) {
                instance = new E03DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E03Base> controller) {
    }

    protected override void DoStartActions(StateController<E03Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E03Base> controller) {
    }

    protected override Transition<E03Base>[] GetTransitions() {
        return null;
    }
}
