using Class_FSM;
using UnityEngine;

public class XMB02CanAttackTransition : XMB02Transition {

    #region Singleton
    public XMB02CanAttackTransition() {

    }
    private static XMB02CanAttackTransition instance = null;
    public static XMB02CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB02CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB02Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.XMB02Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(XMB02AttackState.Instance, this);
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
