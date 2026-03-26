
using Class_FSM;

public class MB03StartState : MB03State {
    #region Singleton
    public MB03StartState() {

    }
    private static MB03StartState instance = null;
    public static MB03StartState Instance {
        get {
            if (instance == null) {
                instance = new MB03StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB03Transition[] transitions = { MB03CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB03Base> controller) {
    }

    protected override void DoStartActions(StateController<MB03Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB03Base> controller) {
    }

    protected override Transition<MB03Base>[] GetTransitions() {
        return transitions;
    }
}
