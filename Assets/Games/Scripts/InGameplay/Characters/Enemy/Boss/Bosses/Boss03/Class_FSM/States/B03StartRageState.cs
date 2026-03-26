

using Class_FSM;

public class B03StartRageState : B03State {
    #region Singleton
    public B03StartRageState() {

    }
    private static B03StartRageState instance = null;
    public static B03StartRageState Instance {
        get {
            if (instance == null) {
                instance = new B03StartRageState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03CanMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.StartLookDown();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
        controller.ObjectBase.LookingDown();
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
