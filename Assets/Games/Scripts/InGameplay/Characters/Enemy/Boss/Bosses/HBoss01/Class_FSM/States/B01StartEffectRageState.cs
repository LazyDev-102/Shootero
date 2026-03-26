

using Class_FSM;

public class HB01StartEffectRageState : HB01State {
    #region Singleton
    public HB01StartEffectRageState() {

    }
    private static HB01StartEffectRageState instance = null;
    public static HB01StartEffectRageState Instance {
        get {
            if (instance == null) {
                instance = new HB01StartEffectRageState();
            }
            return instance;
        }
    }
    #endregion
    private HB01Transition[] transitions = { HB01EndEffectRageTransition.Instance };
    protected override void DoEndActions(StateController<HB01Base> controller) {
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
        controller.ObjectBase.HB01Move.KnockLooking();
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
