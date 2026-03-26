

using Class_FSM;

public class B09RefectorEndPreRageTransition : B09RefectorTransition {
    #region Singleton
    public B09RefectorEndPreRageTransition() {

    }
    private static B09RefectorEndPreRageTransition instance = null;
    public static B09RefectorEndPreRageTransition Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorEndPreRageTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<B09RefectorBase> controller) {
        bool isTransition = !controller.ObjectBase.IsInEffectRage;
        if (isTransition) {
            controller.TransitionToState(B09RefectorRageState.Instance, this);
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
