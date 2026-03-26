

using Class_FSM;

public class E05MoveState : E05State {
    #region Singleton
    public E05MoveState() {

    }
    private static E05MoveState instance = null;
    public static E05MoveState Instance {
        get {
            if (instance == null) {
                instance = new E05MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E05Transition[] transitions = { E05HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E05Base> controller) {
    }

    protected override void DoStartActions(StateController<E05Base> controller) {
        controller.ObjectBase.E05Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E05Base> controller) {
        //E05Move move = controller.ObjectBase.E05Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E05Base>[] GetTransitions() {
        return transitions;
    }
}
