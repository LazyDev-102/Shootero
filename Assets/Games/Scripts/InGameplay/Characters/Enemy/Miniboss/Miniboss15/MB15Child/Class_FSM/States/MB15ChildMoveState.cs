using Class_FSM;
using UnityEngine;

public class MB15ChildMoveState : MB15ChildState {

    #region Singleton
    public MB15ChildMoveState() {

    }
    private static MB15ChildMoveState instance = null;
    public static MB15ChildMoveState Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildMoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ChildTransition[] transitions = { MB15ChildMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.MB15ChildMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB15ChildMove.MoveDirect();
    }

    protected override Transition<MB15ChildBase>[] GetTransitions() {
        return transitions;
    }
}
