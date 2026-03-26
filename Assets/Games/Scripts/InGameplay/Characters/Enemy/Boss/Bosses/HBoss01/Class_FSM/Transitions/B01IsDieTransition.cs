

using Class_FSM;

public class HB01IsDieTransition : HB01Transition {
    #region Singleton
    public HB01IsDieTransition() {

    }
    private static HB01IsDieTransition instance = null;
    public static HB01IsDieTransition Instance {
        get {
            if (instance == null) {
                instance = new HB01IsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if (isTransition) {
            controller.TransitionToState(HB01DeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<HB01Base> controller) {
    }

    public override void DoAfterTransitionActions(StateController<HB01Base> controller) {
    }
}
