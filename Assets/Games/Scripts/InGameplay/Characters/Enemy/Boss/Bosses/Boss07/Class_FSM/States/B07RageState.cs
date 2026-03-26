

using Class_FSM;

public class B07RageState : B07State {
    #region Singleton
    public B07RageState() {

    }
    private static B07RageState instance = null;
    public static B07RageState Instance {
        get {
            if (instance == null) {
                instance = new B07RageState();
            }
            return instance;
        }
    }
    #endregion
    private B07Transition[] transitions = { B07EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B07Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Attack.StartRage();
        controller.ObjectBase.B07Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
