
using Class_FSM;

public class MB14StartState : MB14State {
    #region Singleton
    public MB14StartState() {

    }
    private static MB14StartState instance = null;
    public static MB14StartState Instance {
        get {
            if (instance == null) {
                instance = new MB14StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB14Transition[] transitions = { MB14CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB14Base> controller) {
    }

    protected override void DoStartActions(StateController<MB14Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB14Base> controller) {
    }

    protected override Transition<MB14Base>[] GetTransitions() {
        return transitions;
    }
}
