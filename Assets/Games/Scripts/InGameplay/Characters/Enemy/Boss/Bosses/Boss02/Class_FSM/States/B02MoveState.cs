

using Class_FSM;

public class B02MoveState : B02State {
    #region Singleton
    public B02MoveState() {

    }
    private static B02MoveState instance = null;
    public static B02MoveState Instance {
        get {
            if (instance == null) {
                instance = new B02MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B02Transition[] transitions = { B02EndMoveTransition.Instance, B02CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B02Move.MoveDirect();
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
