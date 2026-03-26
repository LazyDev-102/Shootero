
using Class_FSM;

public class MB01ParentStartState : MB01ParentState {
    #region Singleton
    public MB01ParentStartState() {

    }
    private static MB01ParentStartState instance = null;
    public static MB01ParentStartState Instance {
        get {
            if (instance == null) {
                instance = new MB01ParentStartState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<MB01ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB01ParentBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB01ParentBase> controller) {
    }

    protected override Transition<MB01ParentBase>[] GetTransitions() {
        return null;
    }
}
