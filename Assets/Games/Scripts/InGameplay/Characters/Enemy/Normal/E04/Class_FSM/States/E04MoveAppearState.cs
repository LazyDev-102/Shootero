

using Class_FSM;

public class E04MoveAppearState : E04State {
    #region Singleton
    public E04MoveAppearState() {

    }
    private static E04MoveAppearState instance = null;
    public static E04MoveAppearState Instance {
        get {
            if (instance == null) {
                instance = new E04MoveAppearState();
            }
            return instance;
        }
    }
    #endregion
    private E04Transition[] transitions = { E04HasMoveEndTransition.Instance };

    protected override void DoEndActions(StateController<E04Base> controller) {

    }

    protected override void DoStartActions(StateController<E04Base> controller) {
        E04Move move = controller.ObjectBase.E04Move;
        move.StartMoveAppear();
        move.StartRotateNormal();
    }

    protected override void DoUpdateActions(StateController<E04Base> controller) {
        E04Move move = controller.ObjectBase.E04Move;
        move.RotateSelf();
        //move.MoveDirect();
        //move.CheckingPositionAppearPoint();
    }

    protected override Transition<E04Base>[] GetTransitions() {
        return transitions;
    }
}
