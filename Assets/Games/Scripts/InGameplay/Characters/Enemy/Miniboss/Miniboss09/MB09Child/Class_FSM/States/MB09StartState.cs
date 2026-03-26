
using Class_FSM;

public class MB09StartState : MB09State {
    #region Singleton
    public MB09StartState() {

    }
    private static MB09StartState instance = null;
    public static MB09StartState Instance {
        get {
            if (instance == null) {
                instance = new MB09StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB09Transition[] transitions = { MB09CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB09Base> controller) {
    }

    protected override void DoStartActions(StateController<MB09Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB09Base> controller) {
    }

    protected override Transition<MB09Base>[] GetTransitions() {
        return transitions;
    }
}
