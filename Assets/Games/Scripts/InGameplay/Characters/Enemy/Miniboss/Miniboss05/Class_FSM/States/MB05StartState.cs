
using Class_FSM;

public class MB05StartState : MB05State {
    #region Singleton
    public MB05StartState() {

    }
    private static MB05StartState instance = null;
    public static MB05StartState Instance {
        get {
            if (instance == null) {
                instance = new MB05StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB05Transition[] transitions = { MB05CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB05Base> controller) {
    }

    protected override void DoStartActions(StateController<MB05Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB05Base> controller) {
    }

    protected override Transition<MB05Base>[] GetTransitions() {
        return transitions;
    }
}
