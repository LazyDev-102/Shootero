

using Class_FSM;

public class B01StartEffectRageState : B01State {
    #region Singleton
    public B01StartEffectRageState() {

    }
    private static B01StartEffectRageState instance = null;
    public static B01StartEffectRageState Instance {
        get {
            if (instance == null) {
                instance = new B01StartEffectRageState();
            }
            return instance;
        }
    }
    #endregion
    private B01Transition[] transitions = { B01EndEffectRageTransition.Instance };
    protected override void DoEndActions(StateController<B01Base> controller) {
    }

    protected override void DoStartActions(StateController<B01Base> controller) {
        controller.ObjectBase.StartRage();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {
        controller.ObjectBase.B01Move.KnockLooking();
    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
