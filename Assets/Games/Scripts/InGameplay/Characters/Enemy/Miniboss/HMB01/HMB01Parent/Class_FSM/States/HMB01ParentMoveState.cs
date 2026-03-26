using Class_FSM;
using UnityEngine;

public class HMB01ParentMoveState : HMB01ParentState {

    #region Singleton
    public HMB01ParentMoveState() {

    }
    private static HMB01ParentMoveState instance = null;
    public static HMB01ParentMoveState Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentMoveState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01ParentTransition[] transitions = { HMB01ParentMoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.MinibossMove.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.MinibossMove.MoveDirect();
    }

    protected override Transition<HMB01ParentBase>[] GetTransitions() {
        return transitions;
    }
}
