

using Class_FSM;

public class E11DeadState : E11State {
    #region Singleton
    public E11DeadState() {

    }
    private static E11DeadState instance = null;
    public static E11DeadState Instance {
        get {
            if(instance == null) {
                instance = new E11DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E11Base> controller) {
    }

    protected override void DoStartActions(StateController<E11Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E11Base> controller) {
    }

    protected override Transition<E11Base>[] GetTransitions() {
        return null;
    }
}
