

using Class_FSM;

public class MESpecialB08EndMoveTransition : MESpecialB08Transition {
    #region Singleton
    public MESpecialB08EndMoveTransition() {

    }
    private static MESpecialB08EndMoveTransition instance = null;
    public static MESpecialB08EndMoveTransition Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08EndMoveTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MESpecialB08Base> controller) {
        bool isTransition = controller.ObjectBase.MESpecialB08Move.CompleteMoveToTarget();
        if (isTransition) {
            controller.TransitionToState(MESpecialB08AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MESpecialB08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MESpecialB08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MESpecialB08Base> controller) {
    }
}
