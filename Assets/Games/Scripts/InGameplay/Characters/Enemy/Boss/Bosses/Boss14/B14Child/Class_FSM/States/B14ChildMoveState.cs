using Class_FSM;
using UnityEngine;

public class B14ChildMoveState : B14ChildState {

    #region Singleton
    public B14ChildMoveState() {

    }
    private static B14ChildMoveState instance = null;
    public static B14ChildMoveState Instance {
        get {
            if (instance == null) {
                instance = new B14ChildMoveState();
            }
            return instance;
        }
    }
    #endregion

    private B14ChildTransition[] transitions = { B14ChildMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.B14ChildMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.B14ChildMove.MoveDirect();
    }

    protected override Transition<B14ChildBase>[] GetTransitions() {
        return transitions;
    }
}
