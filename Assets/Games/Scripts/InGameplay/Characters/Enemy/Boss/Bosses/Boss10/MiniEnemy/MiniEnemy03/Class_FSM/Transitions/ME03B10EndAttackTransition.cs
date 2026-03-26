

using Class_FSM;

public class ME03B10EndAttackTransition : ME03B10Transition {

    #region Singleton
    public ME03B10EndAttackTransition() {

    }
    private static ME03B10EndAttackTransition instance = null;
    public static ME03B10EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B10EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME03B10Base> controller) {
        bool isTransition = controller.ObjectBase.ME03B10Attack.IsEndAttack();
        if (isTransition) {
            controller.TransitionToState(ME03B10IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<ME03B10Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<ME03B10Base> controller) {
    }
}
