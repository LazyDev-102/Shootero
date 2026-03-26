

using Class_FSM;

public class XB01EndRageTransition : XB01Transition {

    #region Singleton
    public XB01EndRageTransition() {

    }
    private static XB01EndRageTransition instance = null;
    public static XB01EndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new XB01EndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = !controller.ObjectBase.XB01Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(XB01StartState.Instance, this);
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
