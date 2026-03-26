using Class_FSM;

public class ME03B10MoveState : ME03B10State {

    #region Singleton
    public ME03B10MoveState() {

    }
    private static ME03B10MoveState instance = null;
    public static ME03B10MoveState Instance {
        get {
            if (instance == null) {
                instance = new ME03B10MoveState();
            }
            return instance;
        }
    }
    #endregion

    private ME03B10Transition[] transitions = { ME03B10EndMoveTransition.Instance };

    protected override void DoEndActions(StateController<ME03B10Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B10Base> controller) {
        controller.ObjectBase.ME03B10Move.StartMovePoint();
    }

    protected override void DoUpdateActions(StateController<ME03B10Base> controller) {
    }

    protected override Transition<ME03B10Base>[] GetTransitions() {
        return transitions;

    }
}
