

using Class_FSM;

public class B12EndAttackTransition : B12Transition {
    #region Singleton
    public B12EndAttackTransition() {

    }
    private static B12EndAttackTransition instance = null;
    public static B12EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B12EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B12Base> controller) {
        bool isTransition = !controller.ObjectBase.B12Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B12MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B12Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B12Base> controller) {
    }
}
