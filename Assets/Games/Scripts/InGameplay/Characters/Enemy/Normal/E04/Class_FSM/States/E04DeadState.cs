

using Class_FSM;

public class E04DeadState : E04State{
    #region Singleton
    public E04DeadState() {

    }
    private static E04DeadState instance = null;
    public static E04DeadState Instance {
        get {
            if(instance == null) {
                instance = new E04DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E04Base> controller) {
    }

    protected override void DoStartActions(StateController<E04Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E04Base> controller) {
    }

    protected override Transition<E04Base>[] GetTransitions() {
        return null;
    }
}
