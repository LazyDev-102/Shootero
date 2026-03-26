

using Class_FSM;

public class E12MoveState : E12State {
    #region Singleton
    public E12MoveState() {

    }
    private static E12MoveState instance = null;
    public static E12MoveState Instance {
        get {
            if (instance == null) {
                instance = new E12MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E12Transition[] transitions = { E12HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E12Base> controller) {
    }

    protected override void DoStartActions(StateController<E12Base> controller) {
        controller.ObjectBase.E12Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E12Base> controller) {
        //E12Move move = controller.ObjectBase.E12Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E12Base>[] GetTransitions() {
        return transitions;
    }
}
