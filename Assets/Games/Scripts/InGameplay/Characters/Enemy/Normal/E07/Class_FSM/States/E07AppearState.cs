

using Class_FSM;

public class E07AppearState : E07State {
    #region Singleton
    public E07AppearState() {

    }
    private static E07AppearState instance = null;
    public static E07AppearState Instance {
        get {
            if (instance == null) {
                instance = new E07AppearState();
            }
            return instance;
        }
    }
    #endregion
    private E07Transition[] transitions = { E07EndMoveTransition.Instance, E07HasCompleteKnockTransition.Instance };
    protected override void DoEndActions(StateController<E07Base> controller) {
    }

    protected override void DoStartActions(StateController<E07Base> controller) {
        controller.ObjectBase.E07Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E07Base> controller) {
        //E07Move move = controller.ObjectBase.E07Move;
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E07Base>[] GetTransitions() {
        return transitions;
    }
}
