

using Class_FSM;

public class B10AppearState : B10State {
    #region Singleton
    public B10AppearState() {

    }
    private static B10AppearState instance = null;
    public static B10AppearState Instance {
        get {
            if (instance == null) {
                instance = new B10AppearState();
            }
            return instance;
        }
    }
    #endregion
    private B10Transition[] transitions = { B10EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Move.MoveDirect();
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitions;
    }
}
