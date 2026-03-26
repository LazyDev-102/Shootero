

using Class_FSM;

public class ME03B10CanAttackTransition : ME03B10Transition {

    #region Singleton
    public ME03B10CanAttackTransition() {

    }
    private static ME03B10CanAttackTransition instance = null;
    public static ME03B10CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new ME03B10CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<ME03B10Base> controller) {
        bool isTransition = controller.ObjectBase.ME03B10Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(ME03B10AttackState.Instance, this);
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
