using Class_FSM;

public class XMB01ParentCanAppearTransition : XMB01ParentTransition {

    #region Singleton
    public XMB01ParentCanAppearTransition() {

    }
    private static XMB01ParentCanAppearTransition instance = null;
    public static XMB01ParentCanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new XMB01ParentCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<XMB01ParentBase> controller) {
        bool isTransition = controller.ObjectBase.MinibossMove.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(XMB01ParentAppearState.Instance, this);
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
