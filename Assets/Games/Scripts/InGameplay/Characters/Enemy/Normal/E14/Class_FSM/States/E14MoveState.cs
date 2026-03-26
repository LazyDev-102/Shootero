

using Class_FSM;

public class E14MoveState : E14State {
    #region Singleton
    public E14MoveState() {

    }
    private static E14MoveState instance = null;
    public static E14MoveState Instance {
        get {
            if (instance == null) {
                instance = new E14MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E14Transition[] transitions = { E14HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E14Base> controller) {
    }

    protected override void DoStartActions(StateController<E14Base> controller) {
        controller.ObjectBase.E14Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E14Base> controller) {
    }

    protected override Transition<E14Base>[] GetTransitions() {
        return transitions;
    }
}
