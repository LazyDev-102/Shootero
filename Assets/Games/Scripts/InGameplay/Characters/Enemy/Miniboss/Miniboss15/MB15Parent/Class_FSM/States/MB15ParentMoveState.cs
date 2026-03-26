using Class_FSM;
using UnityEngine;

public class MB15ParentMoveState : MB15ParentState {

    #region Singleton
    public MB15ParentMoveState() {

    }
    private static MB15ParentMoveState instance = null;
    public static MB15ParentMoveState Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentMoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ParentTransition[] transitions = { MB15ParentMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.MB15ParentMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB15ParentMove.MoveDirect();
    }

    protected override Transition<MB15ParentBase>[] GetTransitions() {
        return transitions;
    }
}
