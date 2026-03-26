

using Class_FSM;

public class B01EndAttackTransition : B01Transition {
    #region Singleton
    public B01EndAttackTransition() {

    }
    private static B01EndAttackTransition instance = null;
    public static B01EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B01EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B01Base> controller) {
        bool isTransition = !controller.ObjectBase.B01Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B01MoveState.Instance, this);
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
