

using Class_FSM;

public class HB01MoveCompleteTransition : HB01Transition {
    #region Singleton
    public HB01MoveCompleteTransition() {

    }
    private static HB01MoveCompleteTransition instance = null;
    public static HB01MoveCompleteTransition Instance {
        get {
            if(instance == null) {
                instance = new HB01MoveCompleteTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<HB01Base> controller) {
        bool isTransition = controller.ObjectBase.HB01Move.CompleteMoveToTarget();
        if(isTransition) {
            controller.TransitionToState(HB01IdleState.Instance, this);
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
