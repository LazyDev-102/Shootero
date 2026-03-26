

using Class_FSM;

public class MESpecialB08IdleState : MESpecialB08State {
    #region Singleton
    public MESpecialB08IdleState() {

    }
    private static MESpecialB08IdleState instance = null;
    public static MESpecialB08IdleState Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08IdleState();
            }
            return instance;
        }
    }
    #endregion

    private MESpecialB08Transition[] transitions = { MESpecialB08CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<MESpecialB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MESpecialB08Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<MESpecialB08Base> controller) {
    }

    protected override Transition<MESpecialB08Base>[] GetTransitions() {
        return transitions;
    }
}
