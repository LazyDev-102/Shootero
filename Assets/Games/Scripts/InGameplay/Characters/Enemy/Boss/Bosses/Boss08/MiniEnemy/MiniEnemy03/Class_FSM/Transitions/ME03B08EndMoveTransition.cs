

using Class_FSM;

public class ME03B08EndMoveTransition : ME03B08Transition {
    #region Singleton
    public ME03B08EndMoveTransition() {

    }
    private static ME03B08EndMoveTransition instance = null;
    public static ME03B08EndMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B08EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<ME03B08Base> controller) {
        bool isTransition = controller.ObjectBase.ME03B08Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(ME03B08AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME03B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME03B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME03B08Base> controller) {
    }
}
