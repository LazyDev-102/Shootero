

using Class_FSM;

public class E11MoveState : E11State {
    #region Singleton
    public E11MoveState() {

    }
    private static E11MoveState instance = null;
    public static E11MoveState Instance {
        get {
            if (instance == null) {
                instance = new E11MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E11Transition[] transitions = { E11HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E11Base> controller) {
    }

    protected override void DoStartActions(StateController<E11Base> controller) {
        controller.ObjectBase.E11Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E11Base> controller) {
        //E11Move move = controller.ObjectBase.E11Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E11Base>[] GetTransitions() {
        return transitions;
    }
}
