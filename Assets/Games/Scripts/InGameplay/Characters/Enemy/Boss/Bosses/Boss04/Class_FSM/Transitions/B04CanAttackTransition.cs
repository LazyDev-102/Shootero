

using Class_FSM;

public class B04CanAttackTransition : B04Transition {
    #region Singleton
    public B04CanAttackTransition() {

    }
    private static B04CanAttackTransition instance = null;
    public static B04CanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B04CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B04Base> controller) {
        bool isTransition = controller.ObjectBase.B04Attack.CanAttack() && controller.ObjectBase.IsEndIdle();
        if (isTransition) {
            controller.TransitionToState(B04AttackState.Instance, this);
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
