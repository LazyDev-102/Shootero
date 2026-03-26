
using Class_FSM;

public class MB02StartState : MB02State {
    #region Singleton
    public MB02StartState() {

    }
    private static MB02StartState instance = null;
    public static MB02StartState Instance {
        get {
            if (instance == null) {
                instance = new MB02StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB02Transition[] transitions = { MB02CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB02Base> controller) {
    }

    protected override void DoStartActions(StateController<MB02Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB02Base> controller) {
    }

    protected override Transition<MB02Base>[] GetTransitions() {
        return transitions;
    }
}
