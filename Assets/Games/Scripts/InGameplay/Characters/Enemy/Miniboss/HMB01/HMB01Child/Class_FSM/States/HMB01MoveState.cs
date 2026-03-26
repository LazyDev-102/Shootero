using Class_FSM;
using UnityEngine;

public class HMB01MoveState : HMB01State {

    #region Singleton
    public HMB01MoveState() {

    }
    private static HMB01MoveState instance = null;
    public static HMB01MoveState Instance {
        get {
            if (instance == null) {
                instance = new HMB01MoveState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01Transition[] transitions = { HMB01MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.HMB01Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.HMB01Move.MoveDirect();
    }

    protected override Transition<HMB01Base>[] GetTransitions() {
        return transitions;
    }
}
