
using Class_FSM;

public class HMB01StartState : HMB01State {
    #region Singleton
    public HMB01StartState() {

    }
    private static HMB01StartState instance = null;
    public static HMB01StartState Instance {
        get {
            if (instance == null) {
                instance = new HMB01StartState();
            }
            return instance;
        }
    }
    #endregion

    private HMB01Transition[] transitions = { HMB01CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<HMB01Base> controller) {
    }

    protected override void DoStartActions(StateController<HMB01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<HMB01Base> controller) {
    }

    protected override Transition<HMB01Base>[] GetTransitions() {
        return transitions;
    }
}
