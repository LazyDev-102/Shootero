

using Class_FSM;

public class B04AppearState : B04State {
    #region Singleton
    public B04AppearState() {

    }
    private static B04AppearState instance = null;
    public static B04AppearState Instance {
        get {
            if (instance == null) {
                instance = new B04AppearState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
        //controller.ObjectBase.B04Move.MoveDirectWithWing();
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
