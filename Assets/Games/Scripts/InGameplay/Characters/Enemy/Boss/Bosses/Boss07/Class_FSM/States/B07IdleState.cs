

using Class_FSM;

public class B07IdleState : B07State {
    #region Singleton
    public B07IdleState() {

    }
    private static B07IdleState instance = null;
    public static B07IdleState Instance {
        get {
            if (instance == null) {
                instance = new B07IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B07Transition[] transitions = { B07CanRageTransition.Instance, B07CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
