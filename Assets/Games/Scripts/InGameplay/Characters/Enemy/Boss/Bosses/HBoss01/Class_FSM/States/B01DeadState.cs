

using Class_FSM;

public class HB01DeadState : HB01State {
    #region Singleton
    public HB01DeadState() {

    }
    private static HB01DeadState instance = null;
    public static HB01DeadState Instance {
        get {
            if(instance == null) {
                instance = new HB01DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<HB01Base> controller) {
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return null;
    }
}
