
using Class_FSM;

public class MB16StartState : MB16State {
    #region Singleton
    public MB16StartState() {

    }
    private static MB16StartState instance = null;
    public static MB16StartState Instance {
        get {
            if (instance == null) {
                instance = new MB16StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB16Transition[] transitions = { MB16CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB16Base> controller) {
    }

    protected override void DoStartActions(StateController<MB16Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB16Base> controller) {
    }

    protected override Transition<MB16Base>[] GetTransitions() {
        return transitions;
    }
}
