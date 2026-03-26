

using Class_FSM;

public class MiniShieldDeadState : MiniShieldState {
    #region Singleton
    public MiniShieldDeadState() {

    }
    private static MiniShieldDeadState instance = null;
    public static MiniShieldDeadState Instance {
        get {
            if(instance == null) {
                instance = new MiniShieldDeadState();
            }
            return instance;
        }
    }
    #endregion
    private MiniShieldTransition[] transitions = { MiniShieldIsRestoreTransition.Instance };
    protected override void DoEndActions(StateController<MiniShieldBase> controller) {
    }

    protected override void DoStartActions(StateController<MiniShieldBase> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MiniShieldBase> controller) {
    }

    protected override Transition<MiniShieldBase>[] GetTransitions() {
        return transitions;
    }
}
