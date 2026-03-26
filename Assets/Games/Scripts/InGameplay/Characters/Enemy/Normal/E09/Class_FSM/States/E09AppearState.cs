

using Class_FSM;

public class E09AppearState : E09State {
    #region Singleton
    public E09AppearState() {

    }
    private static E09AppearState instance = null;
    public static E09AppearState Instance {
        get {
            if (instance == null) {
                instance = new E09AppearState();
            }
            return instance;
        }
    }
    #endregion
    private E09Transition[] transitions = { E09EndAppearTransition.Instance };
    protected override void DoEndActions(StateController<E09Base> controller) {
    }

    protected override void DoStartActions(StateController<E09Base> controller) {
        controller.ObjectBase.E09Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E09Base> controller) {
        //E09Move move = controller.ObjectBase.E09Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E09Base>[] GetTransitions() {
        return transitions;
    }
}
