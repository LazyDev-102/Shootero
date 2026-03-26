

using Class_FSM;

public class B02EndAttackTransition : B02Transition {
    #region Singleton
    public B02EndAttackTransition() {

    }
    private static B02EndAttackTransition instance = null;
    public static B02EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B02EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = !controller.ObjectBase.B02Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B02MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B02Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B02Base> controller) {
    }
}
