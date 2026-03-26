

using Class_FSM;

public class B10MoveState : B10State {

    #region Singleton
    public B10MoveState() {

    }
    private static B10MoveState instance = null;
    public static B10MoveState Instance {
        get {
            if (instance == null) {
                instance = new B10MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B10Transition[] transitions = { B10EndMoveTransition.Instance, B10CanRageTransition.Instance };

    protected override void DoEndActions(StateController<B10Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B10Move.MoveDirect();
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
