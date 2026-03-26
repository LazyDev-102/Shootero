

using Class_FSM;

public class B07AppearState : B07State {
    #region Singleton
    public B07AppearState() {

    }
    private static B07AppearState instance = null;
    public static B07AppearState Instance {
        get {
            if (instance == null) {
                instance = new B07AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B07Transition[] transitions = { B07EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Move.MoveDirect();
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
