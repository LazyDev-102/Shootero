

using Class_FSM;

public class B04MoveRageState : B04State {
    #region Singleton
    public B04MoveRageState() {

    }
    private static B04MoveRageState instance = null;
    public static B04MoveRageState Instance {
        get {
            if (instance == null) {
                instance = new B04MoveRageState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04EndMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Move.StartMoveRage();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Move.MoveDirect();
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
