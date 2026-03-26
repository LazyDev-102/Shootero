

using Class_FSM;

public class B02AppearState : B02State {
    #region Singleton
    public B02AppearState() {

    }
    private static B02AppearState instance = null;
    public static B02AppearState Instance {
        get {
            if (instance == null) {
                instance = new B02AppearState();
            }
            return instance;
        }
    }
    #endregion
    private B02Transition[] transitions = { B02EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Move.MoveDirect();
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
