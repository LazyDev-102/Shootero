

using Class_FSM;

public class MESpecialB08MoveState : MESpecialB08State {
    #region Singleton
    public MESpecialB08MoveState() {

    }
    private static MESpecialB08MoveState instance = null;
    public static MESpecialB08MoveState Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08MoveState();
            }
            return instance;
        }
    }
    #endregion
    private MESpecialB08Transition[] transitions = { MESpecialB08EndMoveTransition.Instance };
    protected override void DoEndActions(StateController<MESpecialB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MESpecialB08Base> controller) {
    }

    protected override void DoUpdateActions(StateController<MESpecialB08Base> controller) {
        controller.ObjectBase.MESpecialB08Move.MoveDirect();
    }

    protected override Transition<MESpecialB08Base>[] GetTransitions() {
        return transitions;
    }
}
