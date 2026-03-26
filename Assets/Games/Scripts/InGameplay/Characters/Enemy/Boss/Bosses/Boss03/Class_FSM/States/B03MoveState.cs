

using Class_FSM;

public class B03MoveState : B03State {
    #region Singleton
    public B03MoveState() {

    }
    private static B03MoveState instance = null;
    public static B03MoveState Instance {
        get {
            if (instance == null) {
                instance = new B03MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03EndMoveTransition.Instance, B03CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B03Move.MoveDirect();
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
