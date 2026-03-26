

using Class_FSM;

public class B03EndAttackRageTransition : B03Transition {
    #region Singleton
    public B03EndAttackRageTransition() {

    }
    private static B03EndAttackRageTransition instance = null;
    public static B03EndAttackRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B03EndAttackRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B03Base> controller) {
        bool isTransition = controller.ObjectBase.CanStagger();
        if (isTransition) {
            controller.TransitionToState(B03IdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B03Base> controller) {
        controller.ObjectBase.RestoreAllShield1();
    }

    public override void DoBeforeTransitionActions(StateController<B03Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B03Base> controller) {
    }
}
