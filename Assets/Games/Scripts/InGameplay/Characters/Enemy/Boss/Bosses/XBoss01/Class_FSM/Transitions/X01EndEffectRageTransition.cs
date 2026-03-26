

using Class_FSM;

public class XB01EndEffectRageTransition : XB01Transition {
    #region Singleton
    public XB01EndEffectRageTransition() {

    }
    private static XB01EndEffectRageTransition instance = null;
    public static XB01EndEffectRageTransition Instance {
        get {
            if (instance == null) {
                instance = new XB01EndEffectRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<XB01Base> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(XB01RageState.Instance, this);
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
