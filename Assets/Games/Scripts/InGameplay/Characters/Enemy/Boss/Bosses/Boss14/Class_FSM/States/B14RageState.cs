

using Class_FSM;

public class B14RageState : B14State {

    #region Singleton
    public B14RageState() {

    }
    private static B14RageState instance = null;
    public static B14RageState Instance {
        get {
            if (instance == null) {
                instance = new B14RageState();
            }
            return instance;
        }
    }
    #endregion

    private B14Transition[] transitions = { B14EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B14Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B14Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Attack.StartRage();
        controller.ObjectBase.B14Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
