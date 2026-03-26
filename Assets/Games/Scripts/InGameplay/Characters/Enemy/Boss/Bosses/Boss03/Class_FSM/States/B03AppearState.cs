

using Class_FSM;

public class B03AppearState : B03State {
    #region Singleton
    public B03AppearState() {

    }
    private static B03AppearState instance = null;
    public static B03AppearState Instance {
        get {
            if(instance == null) {
                instance = new B03AppearState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Move.MoveDirect();
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
