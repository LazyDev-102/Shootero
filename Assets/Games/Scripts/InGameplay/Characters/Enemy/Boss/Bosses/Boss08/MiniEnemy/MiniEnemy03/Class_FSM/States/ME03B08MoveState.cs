

using Class_FSM;

public class ME03B08MoveState : ME03B08State {
    #region Singleton
    public ME03B08MoveState() {

    }
    private static ME03B08MoveState instance = null;
    public static ME03B08MoveState Instance {
        get {
            if (instance == null) {
                instance = new ME03B08MoveState();
            }
            return instance;
        }
    }
    #endregion
    private ME03B08Transition[] transitions = { ME03B08EndMoveTransition.Instance };

    protected override void DoEndActions(StateController<ME03B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B08Base> controller) {
        controller.ObjectBase.ME03B08Move.StartTargetPosition();

    }

    protected override void DoUpdateActions(StateController<ME03B08Base> controller) {
        controller.ObjectBase.ME03B08Move.MoveDirect();

    }

    protected override Transition<ME03B08Base>[] GetTransitions() {
        return transitions;
    }
}
