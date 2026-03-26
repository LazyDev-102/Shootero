using Class_FSM;

public class XB01CanRageTransition : XB01Transition {

    #region Singleton
    public XB01CanRageTransition() {

    }
    private static XB01CanRageTransition instance = null;
    public static XB01CanRageTransition Instance {
        get {
            if (instance == null) {
                instance = new XB01CanRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsInRageStatus && !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(XB01StartEffectRageState.Instance, this);
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
