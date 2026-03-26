

using Class_FSM;

public class B04StartRageState : B04State {
    #region Singleton
    public B04StartRageState() {

    }
    private static B04StartRageState instance = null;
    public static B04StartRageState Instance {
        get {
            if (instance == null) {
                instance = new B04StartRageState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04CanMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.StartLookDown();
        controller.ObjectBase.B04Move.StartCloseWings(null);
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
        controller.ObjectBase.LookingDown();
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
