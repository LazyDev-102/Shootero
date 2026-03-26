

using Class_FSM;

public class B01CanAttackTransition : B01Transition {
    #region Singleton
    public B01CanAttackTransition() {

    }
    private static B01CanAttackTransition instance = null;
    public static B01CanAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B01CanAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B01Attack.CanAttack();
        if(isTransition) {
            controller.TransitionToState(B01AttackState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B01Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B01Base> controller) {
    }
}
