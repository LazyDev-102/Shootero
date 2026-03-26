

using Class_FSM;

public class E17MoveState : E17State {
    #region Singleton
    public E17MoveState() {

    }
    private static E17MoveState instance = null;
    public static E17MoveState Instance {
        get {
            if (instance == null) {
                instance = new E17MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E17Transition[] transitions = { E17HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E17Base> controller) {
    }

    protected override void DoStartActions(StateController<E17Base> controller) {
        controller.ObjectBase.E17Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E17Base> controller) {
    }

    protected override Transition<E17Base>[] GetTransitions() {
        return transitions;
    }
}
