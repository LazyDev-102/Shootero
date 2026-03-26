

using Class_FSM;

public class ME02B08EndMoveTransition : ME02B08Transition {
    #region Singleton
    public ME02B08EndMoveTransition() {

    }
    private static ME02B08EndMoveTransition instance = null;
    public static ME02B08EndMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new ME02B08EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME02B08Base> controller) {
        bool isTransition = controller.ObjectBase.ME02B08Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(ME02B08AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME02B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME02B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME02B08Base> controller) {
    }
}
