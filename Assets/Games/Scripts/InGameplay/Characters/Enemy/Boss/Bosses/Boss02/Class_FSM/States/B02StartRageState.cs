

using Class_FSM;

public class B02StartRageState : B02State {
    #region Singleton
    public B02StartRageState() {

    }
    private static B02StartRageState instance = null;
    public static B02StartRageState Instance {
        get {
            if (instance == null) {
                instance = new B02StartRageState();
            }
            return instance;
        }
    }
    #endregion

    private B02Transition[] transitions = { B02CanMoveRageTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.StartLookDown();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
        controller.ObjectBase.LookingDown();
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
