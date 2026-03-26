

using Class_FSM;

public class B09RefectorCanAttackTransition : B09RefectorTransition {
    #region Singleton
    public B09RefectorCanAttackTransition() {

    }
    private static B09RefectorCanAttackTransition instance = null;
    public static B09RefectorCanAttackTransition Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorCanAttackTransition();
            }
            return instance;
        }
    }
    #endregion

    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = controller.ObjectBase.IsEndIdle() && controller.ObjectBase.B09RefectorAttack.CanAttack();
        if (isTransition) {
            controller.TransitionToState(B09RefectorAttackState.Instance, this);
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
