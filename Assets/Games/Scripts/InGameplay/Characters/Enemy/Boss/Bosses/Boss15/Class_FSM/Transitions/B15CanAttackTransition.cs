

using Class_FSM;

public class B15CanAttackTransition : B15Transition {
    #region Singleton
    public B15CanAttackTransition() {

    }
    private static B15CanAttackTransition instance = null;
    public static B15CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B15CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B15Base> controller) {
        bool isTransition = /*controller.ObjectBase.IsEndIdle() && */controller.ObjectBase.B15Attack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B15AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B15Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B15Base> controller) {
    }
}
