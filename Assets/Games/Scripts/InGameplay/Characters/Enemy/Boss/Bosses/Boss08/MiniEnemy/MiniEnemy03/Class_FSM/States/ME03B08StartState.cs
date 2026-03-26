

using Class_FSM;

public class ME03B08StartState : ME03B08State {
    #region Singleton
    public ME03B08StartState() {

    }
    private static ME03B08StartState instance = null;
    public static ME03B08StartState Instance {
        get {
            if (instance == null) {
                instance = new ME03B08StartState();
            }
            return instance;
        }
    }
    #endregion
    private ME03B08Transition[] transitions = { ME03B08CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<ME03B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B08Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<ME03B08Base> controller) {
    }

    protected override Transition<ME03B08Base>[] GetTransitions() {
        return transitions;
    }
}
