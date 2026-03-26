using Class_FSM;
using UnityEngine;

public class XMB01ParentCanAttackTransition : XMB01ParentTransition {

    #region Singleton
    public XMB01ParentCanAttackTransition() {

    }
    private static XMB01ParentCanAttackTransition instance = null;
    public static XMB01ParentCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.XMB01ParentAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(XMB01ParentAttackState.Instance, this);
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
