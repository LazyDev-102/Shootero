

using Class_FSM;

public class B15StartEffectRageState : B15State {
    #region Singleton
    public B15StartEffectRageState() {

    }
    private static B15StartEffectRageState instance = null;
    public static B15StartEffectRageState Instance {
        get {
            if (instance == null) {
                instance = new B15StartEffectRageState();
            }
            return instance;
        }
    }
    #endregion
    private B15Transition[] transitions = { B15EndEffectRageTransition.Instance };
    protected override void DoEndActions(StateController<B15Base> controller) {
        controller.ObjectBase.EndRage();
    }

    protected override void DoStartActions(StateController<B15Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {
        controller.ObjectBase.B15Move.KnockLooking();
    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
