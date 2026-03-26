

using Class_FSM;

public class B03EndAttackTransition : B03Transition {
    #region Singleton
    public B03EndAttackTransition() {

    }
    private static B03EndAttackTransition instance = null;
    public static B03EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B03EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = !controller.ObjectBase.B03Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B03MoveState.Instance, this);
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
