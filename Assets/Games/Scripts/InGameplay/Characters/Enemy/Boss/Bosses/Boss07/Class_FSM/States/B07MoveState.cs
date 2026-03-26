

using Class_FSM;

public class B07MoveState : B07State {
    #region Singleton
    public B07MoveState() {

    }
    private static B07MoveState instance = null;
    public static B07MoveState Instance {
        get {
            if (instance == null) {
                instance = new B07MoveState();
            }
            return instance;
        }
    }
    #endregion

    private B07Transition[] transitions = { B07EndMoveTransition.Instance, B07CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B07Move.MoveDirect();
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
