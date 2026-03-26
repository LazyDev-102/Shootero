

using Class_FSM;

public class B03MoveRageState : B03State {
    #region Singleton
    public B03MoveRageState() {

    }
    private static B03MoveRageState instance = null;
    public static B03MoveRageState Instance {
        get {
            if (instance == null) {
                instance = new B03MoveRageState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03EndMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Move.StartMoveRage();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
