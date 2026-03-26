

using Class_FSM;

public class ME02B08StartState : ME02B08State {
    #region Singleton
    public ME02B08StartState() {

    }
    private static ME02B08StartState instance = null;
    public static ME02B08StartState Instance {
        get {
            if (instance == null) {
                instance = new ME02B08StartState();
            }
            return instance;
        }
    }
    #endregion

    private ME02B08Transition[] transitions = { ME02B08CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<ME02B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME02B08Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<ME02B08Base> controller) {
    }

    protected override Transition<ME02B08Base>[] GetTransitions() {
        return transitions;
    }
}
