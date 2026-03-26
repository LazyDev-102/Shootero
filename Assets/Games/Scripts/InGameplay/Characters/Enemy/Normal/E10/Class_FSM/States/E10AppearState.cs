

using Class_FSM;

public class E10AppearState : E10State {

    #region Singleton
    public E10AppearState() {

    }
    private static E10AppearState instance = null;
    public static E10AppearState Instance {
        get {
            if (instance == null) {
                instance = new E10AppearState();
            }
            return instance;
        }
    }
    #endregion

    private E10Transition[] transitions = { E10EndMoveAppearTransition.Instance };
    protected override void DoEndActions(StateController<E10Base> controller) {
    }

    protected override void DoStartActions(StateController<E10Base> controller) {
        controller.ObjectBase.E10Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E10Base> controller) {
        //E10Move move = controller.ObjectBase.E10Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E10Base>[] GetTransitions() {
        return transitions;
    }
}
