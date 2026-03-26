

using Class_FSM;

public class B04EndAttackRageTransition : B04Transition {
    #region Singleton
    public B04EndAttackRageTransition() {

    }
    private static B04EndAttackRageTransition instance = null;
    public static B04EndAttackRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B04EndAttackRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = !controller.ObjectBase.B04Attack.IsAttacking();
        if (isTransition) {
            controller.TransitionToState(B04MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B04Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B04Base> controller) {
    }
}
