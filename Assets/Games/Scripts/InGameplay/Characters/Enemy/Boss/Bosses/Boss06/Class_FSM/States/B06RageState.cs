

using Class_FSM;

public class B06RageState : B06State {

    #region Singleton
    public B06RageState() {

    }
    private static B06RageState instance = null;
    public static B06RageState Instance {
        get {
            if (instance == null) {
                instance = new B06RageState();
            }
            return instance;
        }
    }
    #endregion

    private B06Transition[] transitions = { B06EndRageTransition.Instance };
    protected override void DoEndActions(StateController<B06Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B06Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Attack.StartRage();
        controller.ObjectBase.B06Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
