

using Class_FSM;

public class B09RefectorDeadState : B09RefectorState {
    #region Singleton
    public B09RefectorDeadState() {

    }
    private static B09RefectorDeadState instance = null;
    public static B09RefectorDeadState Instance {
        get {
            if(instance == null) {
                instance = new B09RefectorDeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return null;
    }
}
