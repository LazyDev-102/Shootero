

using Class_FSM;

public class E06MoveState : E06State {
    #region Singleton
    public E06MoveState() {

    }
    private static E06MoveState instance = null;
    public static E06MoveState Instance {
        get {
            if (instance == null) {
                instance = new E06MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E06Transition[] transitions = { E06HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E06Base> controller) {
    }

    protected override void DoStartActions(StateController<E06Base> controller) {
        controller.ObjectBase.E06Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E06Base> controller) {
        //E06Move move = controller.ObjectBase.E06Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E06Base>[] GetTransitions() {
        return transitions;
    }
}
