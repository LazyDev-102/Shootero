

using Class_FSM;

public class B09RefectorPreRageState : B09RefectorState {
    #region Singleton
    public B09RefectorPreRageState() {

    }
    private static B09RefectorPreRageState instance = null;
    public static B09RefectorPreRageState Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorPreRageState();
            }
            return instance;
        }
    }
    #endregion
    private B09RefectorTransition[] transitions = { B09RefectorEndPreRageTransition.Instance };
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.B09RefectorMove.KnockLooking();
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }

}
