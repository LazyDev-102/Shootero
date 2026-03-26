
using Class_FSM;

public class MB11StartState : MB11State {
    #region Singleton
    public MB11StartState() {

    }
    private static MB11StartState instance = null;
    public static MB11StartState Instance {
        get {
            if (instance == null) {
                instance = new MB11StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB11Transition[] transitions = { MB11CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB11Base> controller) {
    }

    protected override void DoStartActions(StateController<MB11Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB11Base> controller) {
    }

    protected override Transition<MB11Base>[] GetTransitions() {
        return transitions;
    }
}
