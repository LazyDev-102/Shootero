using Class_FSM;
using UnityEngine;

public class XMB01CanAttackTransition : XMB01Transition {

    #region Singleton
    public XMB01CanAttackTransition() {

    }
    private static XMB01CanAttackTransition instance = null;
    public static XMB01CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.XMB01Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(XMB01AttackState.Instance, this);
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
