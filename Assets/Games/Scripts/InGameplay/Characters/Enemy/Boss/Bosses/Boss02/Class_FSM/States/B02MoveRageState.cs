

using Class_FSM;

public class B02MoveRageState : B02State {
    #region Singleton
    public B02MoveRageState() {

    }
    private static B02MoveRageState instance = null;
    public static B02MoveRageState Instance {
        get {
            if (instance == null) {
                instance = new B02MoveRageState();
            }
            return instance;
        }
    }
    #endregion
    private B02Transition[] transitions = { B02EndMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Move.StartMoveRage();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Move.MoveDirect();
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
