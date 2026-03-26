

using Class_FSM;

public class E12DeadState : E12State {
    #region Singleton
    public E12DeadState() {

    }
    private static E12DeadState instance = null;
    public static E12DeadState Instance {
        get {
            if(instance == null) {
                instance = new E12DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E12Base> controller) {
    }

    protected override void DoStartActions(StateController<E12Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E12Base> controller) {
    }

    protected override Transition<E12Base>[] GetTransitions() {
        return null;
    }
}
