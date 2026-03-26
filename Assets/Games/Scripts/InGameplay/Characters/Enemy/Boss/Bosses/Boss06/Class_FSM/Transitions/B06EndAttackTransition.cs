

using Class_FSM;

public class B06EndAttackTransition : B06Transition {
    #region Singleton
    public B06EndAttackTransition() {

    }
    private static B06EndAttackTransition instance = null;
    public static B06EndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B06EndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B06Base> controller) {
        bool isTransition = !controller.ObjectBase.B06Attack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B06MoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B06Base> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B06Base> controller) {
    }
}
