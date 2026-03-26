
using Class_FSM;

public class MB10StartState : MB10State {
    #region Singleton
    public MB10StartState() {

    }
    private static MB10StartState instance = null;
    public static MB10StartState Instance {
        get {
            if (instance == null) {
                instance = new MB10StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB10Transition[] transitions = { MB10CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB10Base> controller) {
    }

    protected override void DoStartActions(StateController<MB10Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB10Base> controller) {
    }

    protected override Transition<MB10Base>[] GetTransitions() {
        return transitions;
    }
}
