using Class_FSM;
using UnityEngine;

public class HMB02MoveState : HMB02State {

    #region Singleton
    public HMB02MoveState() {

    }
    private static HMB02MoveState instance = null;
    public static HMB02MoveState Instance {
        get {
            if (instance == null) {
                instance = new HMB02MoveState();
            }
            return instance;
        }
    }
    #endregion

    private HMB02Transition[] transitions = { HMB02MoveCompleteTransition.Instance };

    protected override void DoEndActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.HMB02Move.StartMoveAfterAttack();

    }

    protected override void DoUpdateActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.HMB02Move.MoveDirect();
    }

    protected override Transition<HMB02Base>[] GetTransitions() {
        return transitions;
    }
}
