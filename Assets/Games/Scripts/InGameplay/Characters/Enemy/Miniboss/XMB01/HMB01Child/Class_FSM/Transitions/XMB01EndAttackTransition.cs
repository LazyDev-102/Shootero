using Class_FSM;
using UnityEngine;

public class XMB01EndAttackTransition : XMB01Transition {

    #region Singleton
    public XMB01EndAttackTransition() {

    }
    private static XMB01EndAttackTransition instance = null;
    public static XMB01EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01Base> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(XMB01MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XMB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XMB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XMB01Base> controller) {
    }
}
