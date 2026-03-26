

using Class_FSM;

public class HB01RageState : HB01State {

    #region Singleton
    public HB01RageState() {

    }
    private static HB01RageState instance = null;
    public static HB01RageState Instance {
        get {
            if (instance == null) {
                instance = new HB01RageState();
            }
            return instance;
        }
    }
    #endregion

    private HB01Transition[] transitions = { HB01EndRageTransition.Instance };
    protected override void DoEndActions(StateController<HB01Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.HB01Attack.EndRage();
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {

        controller.ObjectBase.HB01Attack.StartRage();
        controller.ObjectBase.HB01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
