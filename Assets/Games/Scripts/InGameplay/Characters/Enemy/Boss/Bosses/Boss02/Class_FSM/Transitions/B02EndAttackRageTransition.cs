

using Class_FSM;

public class B02EndAttackRageTransition : B02Transition {
    #region Singleton
    public B02EndAttackRageTransition() {

    }
    private static B02EndAttackRageTransition instance = null;
    public static B02EndAttackRageTransition Instance {
        get {
            if(instance == null) {
                instance = new B02EndAttackRageTransition();
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
