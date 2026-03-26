
using Class_FSM;

public class MB13StartState : MB13State {
    #region Singleton
    public MB13StartState() {

    }
    private static MB13StartState instance = null;
    public static MB13StartState Instance {
        get {
            if (instance == null) {
                instance = new MB13StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB13Transition[] transitions = { MB13CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB13Base> controller) {
    }

    protected override void DoStartActions(StateController<MB13Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB13Base> controller) {
    }

    protected override Transition<MB13Base>[] GetTransitions() {
        return transitions;
    }
}
