

using Class_FSM;

public class B02CanAttackTransition : B02Transition {
    #region Singleton
    public B02CanAttackTransition() {

    }
    private static B02CanAttackTransition instance = null;
    public static B02CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B02CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B02Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B02Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B02AttackState.Instance, this);
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
