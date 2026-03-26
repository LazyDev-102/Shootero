

using Class_FSM;

public class E09DeadState : E09State {
    #region Singleton
    public E09DeadState() {

    }
    private static E09DeadState instance = null;
    public static E09DeadState Instance {
        get {
            if(instance == null) {
                instance = new E09DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E09Base> controller) {
    }

    protected override void DoStartActions(StateController<E09Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E09Base> controller) {
    }

    protected override Transition<E09Base>[] GetTransitions() {
        return null;
    }
}
