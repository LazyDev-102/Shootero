
using Class_FSM;

public class MB07StartState : MB07State {
    #region Singleton
    public MB07StartState() {

    }
    private static MB07StartState instance = null;
    public static MB07StartState Instance {
        get {
            if (instance == null) {
                instance = new MB07StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB07Transition[] transitions = { MB07CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB07Base> controller) {
    }

    protected override void DoStartActions(StateController<MB07Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB07Base> controller) {
    }

    protected override Transition<MB07Base>[] GetTransitions() {
        return transitions;
    }
}
