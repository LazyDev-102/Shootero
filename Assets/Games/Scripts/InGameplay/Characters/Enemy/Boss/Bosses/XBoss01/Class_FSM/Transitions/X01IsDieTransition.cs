

using Class_FSM;

public class XB01IsDieTransition : XB01Transition {
    #region Singleton
    public XB01IsDieTransition() {

    }
    private static XB01IsDieTransition instance = null;
    public static XB01IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new XB01IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(XB01DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<XB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<XB01Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<XB01Base> controller) {
    }
}
