

using Class_FSM;

public class B07MoveRageState : B07State {
    #region Singleton
    public B07MoveRageState() {

    }
    private static B07MoveRageState instance = null;
    public static B07MoveRageState Instance {
        get {
            if (instance == null) {
                instance = new B07MoveRageState();
            }
            return instance;
        }
    }
    #endregion

    private B07Transition[] transitons = { B07EndMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.StartRage();
        controller.ObjectBase.B07Move.StartMoveRage();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Move.MoveDirect();
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitons;
    }
}
