

using Class_FSM;

public class E07DeadState : E07State {
    #region Singleton
    public E07DeadState() {

    }
    private static E07DeadState instance = null;
    public static E07DeadState Instance {
        get {
            if(instance == null) {
                instance = new E07DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E07Base> controller) {
    }

    protected override void DoStartActions(StateController<E07Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E07Base> controller) {
    }

    protected override Transition<E07Base>[] GetTransitions() {
        return null;
    }
}
