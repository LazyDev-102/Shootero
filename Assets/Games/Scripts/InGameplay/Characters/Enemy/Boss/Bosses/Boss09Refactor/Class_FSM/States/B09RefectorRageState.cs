

using Class_FSM;

public class B09RefectorRageState : B09RefectorState {

    #region Singleton
    public B09RefectorRageState() {

    }
    private static B09RefectorRageState instance = null;
    public static B09RefectorRageState Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorRageState();
            }
            return instance;
        }
    }
    #endregion

    private B09RefectorTransition[] transitions = { B09RefectorEndRageTransition.Instance };
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B09RefectorAttack.EndRage();
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {

        controller.ObjectBase.B09RefectorAttack.StartRage();
        controller.ObjectBase.B09RefectorAttack.Attack();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }
}
