

using Class_FSM;

public class XB01MoveCompleteTransition : XB01Transition {
    #region Singleton
    public XB01MoveCompleteTransition() {

    }
    private static XB01MoveCompleteTransition instance = null;
    public static XB01MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new XB01MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = controller.ObjectBase.XB01Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(XB01IdleState.Instance, this);
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
