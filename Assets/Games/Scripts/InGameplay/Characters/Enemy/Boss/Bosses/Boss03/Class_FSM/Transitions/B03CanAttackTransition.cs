

using Class_FSM;

public class B03CanAttackTransition : B03Transition {
    #region Singleton
    public B03CanAttackTransition() {

    }
    private static B03CanAttackTransition instance = null;
    public static B03CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B03CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B03Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B03AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
