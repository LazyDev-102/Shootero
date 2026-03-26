

using Class_FSM;

public class B11DeadState : B11State {
    #region Singleton
    public B11DeadState() {

    }
    private static B11DeadState instance = null;
    public static B11DeadState Instance {
        get {
            if(instance == null) {
                instance = new B11DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B11Base> controller) {
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return null;
    }
}
