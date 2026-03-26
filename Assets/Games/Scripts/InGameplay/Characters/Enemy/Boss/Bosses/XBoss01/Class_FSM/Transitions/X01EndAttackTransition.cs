

using Class_FSM;

public class XB01EndAttackTransition : XB01Transition {
    #region Singleton
    public XB01EndAttackTransition() {

    }
    private static XB01EndAttackTransition instance = null;
    public static XB01EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new XB01EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = !controller.ObjectBase.XB01Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(XB01MoveState.Instance, this);
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
