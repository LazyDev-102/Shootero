

using Class_FSM;

public class B06DeadState : B06State {
    #region Singleton
    public B06DeadState() {

    }
    private static B06DeadState instance = null;
    public static B06DeadState Instance {
        get {
            if(instance == null) {
                instance = new B06DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B06Base> controller) {
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return null;
    }
}
