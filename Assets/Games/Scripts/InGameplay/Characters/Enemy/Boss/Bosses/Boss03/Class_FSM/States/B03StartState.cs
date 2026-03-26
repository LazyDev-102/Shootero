

using Class_FSM;

public class B03StartState : B03State {
    #region Singleton
    public B03StartState() {

    }
    private static B03StartState instance = null;
    public static B03StartState Instance {
        get {
            if(instance == null) {
                instance = new B03StartState();
            }
            return instance;
        }
    }
    #endregion

    private B03Transition[] transitions = { B03CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
