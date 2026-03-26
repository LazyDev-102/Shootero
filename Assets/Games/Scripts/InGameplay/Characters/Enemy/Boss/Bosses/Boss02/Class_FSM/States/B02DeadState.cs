

using Class_FSM;

public class B02DeadState : B02State {
    #region Singleton
    public B02DeadState() {

    }
    private static B02DeadState instance = null;
    public static B02DeadState Instance {
        get {
            if(instance == null) {
                instance = new B02DeadState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<B02Base> controller) {
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return null;
    }
}
