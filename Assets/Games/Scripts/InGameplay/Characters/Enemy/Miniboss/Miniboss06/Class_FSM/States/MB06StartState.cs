
using Class_FSM;

public class MB06StartState : MB06State {
    #region Singleton
    public MB06StartState() {

    }
    private static MB06StartState instance = null;
    public static MB06StartState Instance {
        get {
            if (instance == null) {
                instance = new MB06StartState();
            }
            return instance;
        }
    }
    #endregion

    private MB06Transition[] transitions = { MB06CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<MB06Base> controller) {
    }

    protected override void DoStartActions(StateController<MB06Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MB06Base> controller) {
    }

    protected override Transition<MB06Base>[] GetTransitions() {
        return transitions;
    }
}
