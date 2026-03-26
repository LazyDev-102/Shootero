

using Class_FSM;

public class XB01CanAttackTransition : XB01Transition {
    #region Singleton
    public XB01CanAttackTransition() {

    }
    private static XB01CanAttackTransition instance = null;
    public static XB01CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new XB01CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.XB01Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(XB01AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<XB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<XB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XB01Base> controller) {
    }
}
