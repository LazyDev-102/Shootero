
using Class_FSM;

public class MB01StartState : MB01State {
    #region Singleton
    public MB01StartState() {

    }
    private static MB01StartState instance = null;
    public static MB01StartState Instance {
        get {
            if (instance == null) {
                instance = new MB01StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB01Transition[] transitions = { MB01CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB01Base> controller) {
    }

    protected override void DoStartActions(StateController<MB01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB01Base> controller) {
    }

    protected override Transition<MB01Base>[] GetTransitions() {
        return transitions;
    }
}
