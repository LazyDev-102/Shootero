

using Class_FSM;

public class E08DeadState : E08State {
    #region Singleton
    public E08DeadState() {

    }
    private static E08DeadState instance = null;
    public static E08DeadState Instance {
        get {
            if(instance == null) {
                instance = new E08DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E08Base> controller) {
    }

    protected override void DoStartActions(StateController<E08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E08Base> controller) {
    }

    protected override Transition<E08Base>[] GetTransitions() {
        return null;
    }
}
