using Class_FSM;
using UnityEngine;

public class MB01ParentMoveState : MB01ParentState {

    #region Singleton
    public MB01ParentMoveState() {

    }
    private static MB01ParentMoveState instance = null;
    public static MB01ParentMoveState Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentMoveState();
            }
            return instance;
        }
    }
    #endregion

    private MB01ParentTransition[] transitions = { MB01ParentMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.MB01ParentMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MB01ParentMove.MoveDirect();
    }

    protected override Transition<MB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
