using Class_FSM;
using UnityEngine;

public class XMB01ParentEndAttackTransition : XMB01ParentTransition {

    #region Singleton
    public XMB01ParentEndAttackTransition() {

    }
    private static XMB01ParentEndAttackTransition instance = null;
    public static XMB01ParentEndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01ParentBase> controller) {
        bool isTransition = !controller.ObjectBase.MinibossAttack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(XMB01ParentMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XMB01ParentBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XMB01ParentBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XMB01ParentBase> controller) {
    }
}
