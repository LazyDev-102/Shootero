

using Class_FSM;

public class B01RageState : B01State {

    #region Singleton
    public B01RageState() {

    }
    private static B01RageState instance = null;
    public static B01RageState Instance {
        get {
            if (instance == null) {
                instance = new B01RageState();
            }
            return instance;
        }
    }
    #endregion

    private B01Transition[] transitions = { B01EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B01Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B01Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B01Base> controller) {

        controller.ObjectBase.B01Attack.StartRage();
        controller.ObjectBase.B01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
