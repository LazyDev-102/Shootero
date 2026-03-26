using Class_FSM;
using UnityEngine;

public class MB09ParentMoveState : MB09ParentState {

    #region Singleton
    public MB09ParentMoveState() {

    }
    private static MB09ParentMoveState instance = null;
    public static MB09ParentMoveState Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentMoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB09ParentTransition[] transitions = { MB09ParentMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.MB09ParentMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB09ParentMove.MoveDirect();
    }

    protected override Transition<MB09ParentBase>[] GetTransitions() {
        return transitions;
    }
}
