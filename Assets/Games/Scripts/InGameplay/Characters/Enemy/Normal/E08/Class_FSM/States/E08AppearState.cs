

using Class_FSM;

public class E08AppearState : E08State {
    #region Singleton
    public E08AppearState() {

    }
    private static E08AppearState instance = null;
    public static E08AppearState Instance {
        get {
            if (instance == null) {
                instance = new E08AppearState();
            }
            return instance;
        }
    }
    #endregion
    private E08Transition[] transitions = { E08EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<E08Base> controller) {
    }

    protected override void DoStartActions(StateController<E08Base> controller) {
        controller.ObjectBase.E08Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E08Base> controller) {
        //E08Move move = controller.ObjectBase.E08Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E08Base>[] GetTransitions() {
        return transitions;
    }
}
