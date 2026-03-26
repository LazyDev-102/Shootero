

using Class_FSM;

public class MiniShieldIsDeadTransition : MiniShieldTransition {
    #region Singleton
    public MiniShieldIsDeadTransition() {

    }
    private static MiniShieldIsDeadTransition instance = null;
    public static MiniShieldIsDeadTransition Instance {
        get {
            if(instance == null) {
                instance = new MiniShieldIsDeadTransition();
            }
            return instance;
        }
    }
    #endregion
    public override bool CheckTransition(StateController<MiniShieldBase> controller) {
        bool isTransition = controller.ObjectBase.IsDie();
        if(isTransition) {
            controller.TransitionToState(MiniShieldDeadState.Instance, this);
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
