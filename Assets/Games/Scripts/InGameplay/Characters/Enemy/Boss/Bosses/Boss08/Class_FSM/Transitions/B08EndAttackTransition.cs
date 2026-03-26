

using Class_FSM;

public class B08EndAttackTransition : B08Transition {
    #region Singleton
    public B08EndAttackTransition() {

    }
    private static B08EndAttackTransition instance = null;
    public static B08EndAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B08EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B08Base> controller) {
        bool isTransition = !controller.ObjectBase.B08Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B08MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B08Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B08Base> controller) {
    }
}
