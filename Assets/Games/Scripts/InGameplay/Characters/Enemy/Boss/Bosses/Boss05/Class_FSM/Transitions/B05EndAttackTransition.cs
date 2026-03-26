

using Class_FSM;

public class B05EndAttackTransition : B05Transition {
    #region Singleton
    public B05EndAttackTransition() {

    }
    private static B05EndAttackTransition instance = null;
    public static B05EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B05EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B05Base> controller) {
        bool isTransition = !controller.ObjectBase.B05Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B05MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B05Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B05Base> controller) {
    }
}
