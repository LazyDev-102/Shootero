

using Class_FSM;

public class B09RefectorEndAttackTransition : B09RefectorTransition {
    #region Singleton
    public B09RefectorEndAttackTransition() {

    }
    private static B09RefectorEndAttackTransition instance = null;
    public static B09RefectorEndAttackTransition Instance {
        get {
            if(instance == null) {
                instance = new B09RefectorEndAttackTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = !controller.ObjectBase.B09RefectorAttack.IsAttacking();
        if(isTransition) {
            controller.TransitionToState(B09RefectorMoveState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09RefectorBase> controller) {
    }
}
