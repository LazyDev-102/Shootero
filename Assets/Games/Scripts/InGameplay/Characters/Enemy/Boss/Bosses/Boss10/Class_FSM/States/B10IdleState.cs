

using Class_FSM;

public class B10IdleState : B10State {
    #region Singleton
    public B10IdleState() {

    }
    private static B10IdleState instance = null;
    public static B10IdleState Instance {
        get {
            if (instance == null) {
                instance = new B10IdleState();
            }
            return instance;
        }
    }
    #endregion

    private B10Transition[] transitions = { B10CanRageTransition.Instance, B10CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
