

using Class_FSM;

public class ME02B08MoveState : ME02B08State {
    #region Singleton
    public ME02B08MoveState() {

    }
    private static ME02B08MoveState instance = null;
    public static ME02B08MoveState Instance {
        get {
            if (instance == null) {
                instance = new ME02B08MoveState();
            }
            return instance;
        }
    }
    #endregion

    private ME02B08Transition[] transitions = { ME02B08EndMoveTransition.Instance };
    protected override void DoEndActions(StateController<ME02B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME02B08Base> controller) {
        controller.ObjectBase.ME02B08Move.StartTargetPosition();
    }

    protected override void DoUpdateActions(StateController<ME02B08Base> controller) {
        controller.ObjectBase.ME02B08Move.MoveDirect();
    }

    protected override Transition<ME02B08Base>[] GetTransitions() {
        return transitions;
    }
}
