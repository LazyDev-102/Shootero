

using Class_FSM;

public class B05RageState : B05State {

    #region Singleton
    public B05RageState() {

    }
    private static B05RageState instance = null;
    public static B05RageState Instance {
        get {
            if (instance == null) {
                instance = new B05RageState();
            }
            return instance;
        }
    }
    #endregion

    private B05Transition[] transitions = { B05EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B05Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B05Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Attack.StartRage();
        controller.ObjectBase.B05Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
