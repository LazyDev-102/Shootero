

using Class_FSM;

public class E15MoveState : E15State {
    #region Singleton
    public E15MoveState() {

    }
    private static E15MoveState instance = null;
    public static E15MoveState Instance {
        get {
            if (instance == null) {
                instance = new E15MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E15Transition[] transitions = { E15HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E15Base> controller) {
    }

    protected override void DoStartActions(StateController<E15Base> controller) {
        controller.ObjectBase.E15Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E15Base> controller) {
    }

    protected override Transition<E15Base>[] GetTransitions() {
        return transitions;
    }
}
