
using Class_FSM;

public class MB12StartState : MB12State {
    #region Singleton
    public MB12StartState() {

    }
    private static MB12StartState instance = null;
    public static MB12StartState Instance {
        get {
            if (instance == null) {
                instance = new MB12StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB12Transition[] transitions = { MB12CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB12Base> controller) {
    }

    protected override void DoStartActions(StateController<MB12Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB12Base> controller) {
    }

    protected override Transition<MB12Base>[] GetTransitions() {
        return transitions;
    }
}
