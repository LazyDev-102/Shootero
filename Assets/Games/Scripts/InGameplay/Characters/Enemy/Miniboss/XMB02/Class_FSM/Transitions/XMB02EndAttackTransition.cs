using Class_FSM;
using UnityEngine;

public class XMB02EndAttackTransition : XMB02Transition {

    #region Singleton
    public XMB02EndAttackTransition() {

    }
    private static XMB02EndAttackTransition instance = null;
    public static XMB02EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB02EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB02Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(XMB02MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XMB02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XMB02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XMB02Base> controller) {
    }
}
