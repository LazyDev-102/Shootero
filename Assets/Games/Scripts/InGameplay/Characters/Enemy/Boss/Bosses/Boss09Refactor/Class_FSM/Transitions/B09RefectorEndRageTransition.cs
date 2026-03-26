

using Class_FSM;

public class B09RefectorEndRageTransition : B09RefectorTransition {

    #region Singleton
    public B09RefectorEndRageTransition() {

    }
    private static B09RefectorEndRageTransition instance = null;
    public static B09RefectorEndRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorEndRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = !controller.ObjectBase.B09RefectorAttack.IsAttacking();
        if (isTransition) {
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
