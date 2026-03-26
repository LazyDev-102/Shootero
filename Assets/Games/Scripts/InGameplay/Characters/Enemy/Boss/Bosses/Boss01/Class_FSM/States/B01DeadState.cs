

using Class_FSM;

public class B01DeadState : B01State {
    #region Singleton
    public B01DeadState() {

    }
    private static B01DeadState instance = null;
    public static B01DeadState Instance {
        get {
            if(instance == null) {
                instance = new B01DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B01Base> controller) {
    }

    protected override void DoStartActions(StateController<B01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return null;
    }
}
