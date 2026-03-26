

using Class_FSM;

public class E13MoveState : E13State {
    #region Singleton
    public E13MoveState() {

    }
    private static E13MoveState instance = null;
    public static E13MoveState Instance {
        get {
            if (instance == null) {
                instance = new E13MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E13Transition[] transitions = { E13HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E13Base> controller) {
    }

    protected override void DoStartActions(StateController<E13Base> controller) {
        controller.ObjectBase.E13Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E13Base> controller) {
    }

    protected override Transition<E13Base>[] GetTransitions() {
        return transitions;
    }
}
