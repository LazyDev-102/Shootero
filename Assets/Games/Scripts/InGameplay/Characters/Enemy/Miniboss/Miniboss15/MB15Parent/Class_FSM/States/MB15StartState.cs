
using Class_FSM;

public class MB15ParentStartState : MB15ParentState {
    #region Singleton
    public MB15ParentStartState() {

    }
    private static MB15ParentStartState instance = null;
    public static MB15ParentStartState Instance {
        get {
            if (instance == null) {
                instance = new MB15ParentStartState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<MB15ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB15ParentBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB15ParentBase> controller) {
    }

    protected override Transition<MB15ParentBase>[] GetTransitions() {
        return null;
    }
}
