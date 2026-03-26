

using Class_FSM;

public class HB01CanAppearTransition : HB01Transition {
    #region Singleton
    public HB01CanAppearTransition() {

    }
    private static HB01CanAppearTransition instance = null;
    public static HB01CanAppearTransition Instance {
        get {
            if (instance == null) {
                instance = new HB01CanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = controller.ObjectBase.HB01Move.CanMoveAppear();
        if (isTransition) {
            controller.TransitionToState(HB01AppearState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HB01Base> controller) {
    }
}
