
using Class_FSM;

public class MB15ChildStartState : MB15ChildState {
    #region Singleton
    public MB15ChildStartState() {

    }
    private static MB15ChildStartState instance = null;
    public static MB15ChildStartState Instance {
        get {
            if (instance == null) {
                instance = new MB15ChildStartState();
            }
            return instance;
        }
    }
    #endregion

    private MB15ChildTransition[] transitions = { MB15ChildCanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB15ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<MB15ChildBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB15ChildBase> controller) {
    }

    protected override Transition<MB15ChildBase>[] GetTransitions() {
        return transitions;
    }
}
