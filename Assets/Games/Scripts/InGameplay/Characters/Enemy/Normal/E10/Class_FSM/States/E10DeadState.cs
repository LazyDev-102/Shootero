

using Class_FSM;

public class E10DeadState : E10State {
    #region Singleton
    public E10DeadState() {

    }
    private static E10DeadState instance = null;
    public static E10DeadState Instance {
        get {
            if(instance == null) {
                instance = new E10DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<E10Base> controller) {
    }

    protected override void DoStartActions(StateController<E10Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E10Base> controller) {
    }

    protected override Transition<E10Base>[] GetTransitions() {
        return null;
    }
}
