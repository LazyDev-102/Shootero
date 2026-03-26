

using Class_FSM;

public class E02DeadState : E02State {

    #region Singleton
    public E02DeadState() {

    }
    private static E02DeadState instance = null;
    public static E02DeadState Instance {
        get {
            if(instance == null) {
                instance = new E02DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E02Base> controller) {
    }

    protected override void DoStartActions(StateController<E02Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E02Base> controller) {
    }

    protected override Transition<E02Base>[] GetTransitions() {
        return null;
    }
}
