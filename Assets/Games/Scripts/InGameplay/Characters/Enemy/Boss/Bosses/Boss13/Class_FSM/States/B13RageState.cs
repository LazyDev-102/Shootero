

using Class_FSM;

public class B13RageState : B13State {

    #region Singleton
    public B13RageState() {

    }
    private static B13RageState instance = null;
    public static B13RageState Instance {
        get {
            if (instance == null) {
                instance = new B13RageState();
            }
            return instance;
        }
    }
    #endregion

    private B13Transition[] transitions = { B13EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B13Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B13Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Attack.StartRage();
        controller.ObjectBase.B13Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
