

using Class_FSM;

public class B08IdleState : B08State {
    #region Singleton
    public B08IdleState() {

    }
    private static B08IdleState instance = null;
    public static B08IdleState Instance {
        get {
            if (instance == null) {
                instance = new B08IdleState();
            }
            return instance;
        }
    }
    #endregion

    private B08Transition[] transitions = { B08CanRageTransition.Instance, B08CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;
    }
}
