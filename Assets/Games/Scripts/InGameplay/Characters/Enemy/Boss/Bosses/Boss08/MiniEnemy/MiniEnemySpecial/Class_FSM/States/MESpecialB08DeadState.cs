

using Class_FSM;

public class MESpecialB08DeadState : MESpecialB08State {
    #region Singleton
    public MESpecialB08DeadState() {

    }
    private static MESpecialB08DeadState instance = null;
    public static MESpecialB08DeadState Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<MESpecialB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MESpecialB08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MESpecialB08Base> controller) {
    }

    protected override Transition<MESpecialB08Base>[] GetTransitions() {
        return null;
    }
}
