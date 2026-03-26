

using Class_FSM;

public class B09RefectorIsDieTransition : B09RefectorTransition {
    #region Singleton
    public B09RefectorIsDieTransition() {

    }
    private static B09RefectorIsDieTransition instance = null;
    public static B09RefectorIsDieTransition Instance {
        get {
            if(instance == null) {
                instance = new B09RefectorIsDieTransition();
            }
            return instance;
        }
    }


    #endregion

    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(B09RefectorDeadState.Instance, this);
        }
        return isTransition;
    }

    public override void DoBeforeTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<B09RefectorBase> controller) {
    }

    public override void DoAfterTransitionActions(StateController<B09RefectorBase> controller) {
    }
}
