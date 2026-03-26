
using Class_FSM;

public class HMB02StartState : HMB02State {
    #region Singleton
    public HMB02StartState() {

    }
    private static HMB02StartState instance = null;
    public static HMB02StartState Instance {
        get {
            if (instance == null) {
                instance = new HMB02StartState();
            }
            return instance;
        }
    }
    #endregion

    private HMB02Transition[] transitions = { HMB02CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<HMB02Base> controller) {
    }

    protected override void DoStartActions(StateController<HMB02Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<HMB02Base> controller) {
    }

    protected override Transition<HMB02Base>[] GetTransitions() {
        return transitions;
    }
}
