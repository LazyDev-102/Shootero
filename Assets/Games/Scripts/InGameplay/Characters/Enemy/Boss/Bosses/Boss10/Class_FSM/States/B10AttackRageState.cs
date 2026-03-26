

using Class_FSM;

public class B10AttackRageState : B10State {
    #region Singleton
    public B10AttackRageState() {

    }
    private static B10AttackRageState instance = null;
    public static B10AttackRageState Instance {
        get {
            if (instance == null) {
                instance = new B10AttackRageState();
            }
            return instance;
        }
    }
    #endregion
    private B10Transition[] transitions = { B10EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B10Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Attack.StartRage();
        controller.ObjectBase.B10Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
