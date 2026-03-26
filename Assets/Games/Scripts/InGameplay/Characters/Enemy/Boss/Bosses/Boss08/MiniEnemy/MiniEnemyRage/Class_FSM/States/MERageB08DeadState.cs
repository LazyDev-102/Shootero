

using Class_FSM;

public class MERageB08DeadState : MERageB08State {
    #region Singleton
    public MERageB08DeadState() {

    }
    private static MERageB08DeadState instance = null;
    public static MERageB08DeadState Instance {
        get {
            if (instance == null) {
                instance = new MERageB08DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<MERageB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MERageB08Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MERageB08Base> controller) {
    }

    protected override Transition<MERageB08Base>[] GetTransitions() {
        return null;
    }
}
