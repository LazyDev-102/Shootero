

using Class_FSM;

public class B11EndAttackTransition : B11Transition {
    #region Singleton
    public B11EndAttackTransition() {

    }
    private static B11EndAttackTransition instance = null;
    public static B11EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B11EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B11Base> controller) {
        bool isTransition = !controller.ObjectBase.B11Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B11MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B11Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B11Base> controller) {
    }
}
