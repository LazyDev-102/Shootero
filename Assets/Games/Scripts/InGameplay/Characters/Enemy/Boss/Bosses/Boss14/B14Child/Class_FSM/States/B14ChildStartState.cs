
using Class_FSM;

public class B14ChildStartState : B14ChildState {
    #region Singleton
    public B14ChildStartState() {

    }
    private static B14ChildStartState instance = null;
    public static B14ChildStartState Instance {
        get {
            if (instance == null) {
                instance = new B14ChildStartState();
            }
            return instance;
        }
    }
    #endregion

    private B14ChildTransition[] transitions = { B14ChildCanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B14ChildBase> controller) {
    }

    protected override void DoStartActions(StateController<B14ChildBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B14ChildBase> controller) {
    }

    protected override Transition<B14ChildBase>[] GetTransitions() {
        return transitions;
    }
}
