

using Class_FSM;

public class MiniShieldIdleState : MiniShieldState {
    #region Singleton
    public MiniShieldIdleState() {

    }
    private static MiniShieldIdleState instance = null;
    public static MiniShieldIdleState Instance {
        get {
            if(instance == null) {
                instance = new MiniShieldIdleState();
            }
            return instance;
        }
    }
    #endregion
    private MiniShieldTransition[] transitions = { MiniShieldIsDeadTransition.Instance };
    protected override void DoEndActions(StateController<MiniShieldBase> controller) {
    }

    protected override void DoStartActions(StateController<MiniShieldBase> controller) {
    }

    protected override void DoUpdateActions(StateController<MiniShieldBase> controller) {
    }

    protected override Transition<MiniShieldBase>[] GetTransitions() {
        return transitions;
    }
}
