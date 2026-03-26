

using Class_FSM;

public class B12ChildMoveState : B12ChildState {
    #region Singleton
    public B12ChildMoveState() {

    }
    private static B12ChildMoveState instance = null;
    public static B12ChildMoveState Instance {
        get {
            if (instance == null) {
                instance = new B12ChildMoveState();
            }
            return instance;
        }
    }
    #endregion

    private B12ChildTransition[] transitions = { B12ChildHasOutBoundTransiton.Instance, B12ChildHasCompleteKnockTransition.Instance };
    protected override void DoEndActions(StateController<B12ChildBase> controller) {
        //controller.ObjectBase.B12ChildMove.HideMoveTrail();
        controller.ObjectBase.B12ChildMove.EndTargetMoveAttack();
    }

    protected override void DoStartActions(StateController<B12ChildBase> controller) {
    }

    protected override void DoUpdateActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.B12ChildMove.MoveDirect();
    }

    protected override Transition<B12ChildBase>[] GetTransitions() {
        return transitions;
    }
}
