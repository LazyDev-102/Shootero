

using Class_FSM;

public class B09RageState : B09State {

    #region Singleton
    public B09RageState() {

    }
    private static B09RageState instance = null;
    public static B09RageState Instance {
        get {
            if (instance == null) {
                instance = new B09RageState();
            }
            return instance;
        }
    }
    #endregion

    private B09Transition[] transitions = { B09EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B09Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B09Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Attack.StartRage();
        controller.ObjectBase.B09Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}
