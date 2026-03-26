

using Class_FSM;

public class B09RefectorCanAppearTransition : B09RefectorTransition {
    #region Singleton
    public B09RefectorCanAppearTransition() {

    }
    private static B09RefectorCanAppearTransition instance = null;
    public static B09RefectorCanAppearTransition Instance {
        get {
            if(instance == null) {
                instance = new B09RefectorCanAppearTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = controller.ObjectBase.B09RefectorMove.CanMoveAppear();
        if(isTransition) {
            controller.TransitionToState(B09RefectorAppearState.Instance, this);
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
