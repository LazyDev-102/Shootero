

using Class_FSM;

public class B08RageState : B08State {
    #region Singleton
    public B08RageState() {

    }
    private static B08RageState instance = null;
    public static B08RageState Instance {
        get {
            if (instance == null) {
                instance = new B08RageState();
            }
            return instance;
        }
    }
    #endregion

    private B08Transition[] transitions = { B08EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B08Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.B08Attack.StartRage();
        controller.ObjectBase.B08Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {

    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;
    }
}
