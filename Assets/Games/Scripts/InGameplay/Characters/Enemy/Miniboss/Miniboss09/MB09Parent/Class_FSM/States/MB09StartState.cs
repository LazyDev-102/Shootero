
using Class_FSM;

public class MB09ParentStartState : MB09ParentState {
    #region Singleton
    public MB09ParentStartState() {

    }
    private static MB09ParentStartState instance = null;
    public static MB09ParentStartState Instance {
        get {
            if (instance == null) {
                instance = new MB09ParentStartState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<MB09ParentBase> controller) {
    }

    protected override void DoStartActions(StateController<MB09ParentBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB09ParentBase> controller) {
    }

    protected override Transition<MB09ParentBase>[] GetTransitions() {
        return null;
    }
}
