

using Class_FSM;

public class B12ChildDeadState : B12ChildState {

    #region Singleton
    public B12ChildDeadState() {

    }
    private static B12ChildDeadState instance = null;
    public static B12ChildDeadState Instance {
        get {
            if(instance == null) {
                instance = new B12ChildDeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B12ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B12ChildBase> controller) {
    }

    protected override Transition<B12ChildBase>[] GetTransitions() {
        return null;
    }
}
