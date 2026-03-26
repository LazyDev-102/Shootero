
using Class_FSM;

public class HMB01ParentStartState : HMB01ParentState {
    #region Singleton
    public HMB01ParentStartState() {

    }
    private static HMB01ParentStartState instance = null;
    public static HMB01ParentStartState Instance {
        get {
            if (instance == null) {
                instance = new HMB01ParentStartState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<HMB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<HMB01ParentBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<HMB01ParentBase> controller) {
    }

    protected override Transition<HMB01ParentBase>[] GetTransitions() {
        return null;
    }
}
