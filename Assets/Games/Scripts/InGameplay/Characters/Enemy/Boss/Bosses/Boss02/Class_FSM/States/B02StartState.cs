

using Class_FSM;

public class B02StartState : B02State {
    #region Singleton
    public B02StartState() {

    }
    private static B02StartState instance = null;
    public static B02StartState Instance {
        get {
            if(instance == null) {
                instance = new B02StartState();
            }
            return instance;
        }
    }
    #endregion 
    private B02Transition[] transitions = { B02CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
