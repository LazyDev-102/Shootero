

using Class_FSM;

public class MiniShieldIsRestoreTransition : MiniShieldTransition {
    #region Singleton
    public MiniShieldIsRestoreTransition() {

    }
    private static MiniShieldIsRestoreTransition instance = null;
    public static MiniShieldIsRestoreTransition Instance {
        get {
            if(instance == null) {
                instance = new MiniShieldIsRestoreTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MiniShieldBase> controller) {
        bool isTransition = !controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(MiniShieldIdleState.Instance, this);
        }
        return isTransition;
    }

    public override void DoAfterTransitionActions(StateController<MiniShieldBase> controller) {
    }

    public override void DoBeforeTransitionActions(StateController<MiniShieldBase> controller) {
    }

    public override void DoWhileTransitionActions(StateController<MiniShieldBase> controller) {
    }
}
