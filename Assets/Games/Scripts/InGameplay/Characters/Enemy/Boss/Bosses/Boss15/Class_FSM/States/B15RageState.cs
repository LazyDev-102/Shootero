

using Class_FSM;

public class B15RageState : B15State {

    #region Singleton
    public B15RageState() {

    }
    private static B15RageState instance = null;
    public static B15RageState Instance {
        get {
            if (instance == null) {
                instance = new B15RageState();
            }
            return instance;
        }
    }
    #endregion

    private B15Transition[] transitions = { B15EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B15Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B15Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B15Base> controller) {

        controller.ObjectBase.B15Attack.StartRage();
        controller.ObjectBase.B15Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
