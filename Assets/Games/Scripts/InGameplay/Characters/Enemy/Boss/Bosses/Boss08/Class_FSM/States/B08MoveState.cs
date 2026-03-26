

using Class_FSM;

public class B08MoveState : B08State {
    #region Singleton
    public B08MoveState() {

    }
    private static B08MoveState instance = null;
    public static B08MoveState Instance {
        get {
            if (instance == null) {
                instance = new B08MoveState();
            }
            return instance;
        }
    }
    #endregion

    private B08Transition[] transitions = { B08EndMoveTransition.Instance, B08CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.B08Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B08Move.MoveDirect();
    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;
    }
}
