
using Class_FSM;

public class MB17StartState : MB17State {
    #region Singleton
    public MB17StartState() {

    }
    private static MB17StartState instance = null;
    public static MB17StartState Instance {
        get {
            if (instance == null) {
                instance = new MB17StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB17Transition[] transitions = { MB17CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB17Base> controller) {
    }

    protected override void DoStartActions(StateController<MB17Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB17Base> controller) {
    }

    protected override Transition<MB17Base>[] GetTransitions() {
        return transitions;
    }
}
