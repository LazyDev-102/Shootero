

using Class_FSM;

public class B08AppearState : B08State {
    #region Singleton
    public B08AppearState() {

    }
    private static B08AppearState instance = null;
    public static B08AppearState Instance {
        get {
            if (instance == null) {
                instance = new B08AppearState();
            }
            return instance;
        }
    }
    #endregion
    private B08Transition[] transitions = { B08EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.B08Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {
        controller.ObjectBase.B08Move.MoveDirect();
    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;
    }
}
