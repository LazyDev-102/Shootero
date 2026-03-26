

using Class_FSM;

public class E03MoveAppearState : E03State {
    #region Singleton
    public E03MoveAppearState() {

    }
    private static E03MoveAppearState instance = null;
    public static E03MoveAppearState Instance {
        get {
            if (instance == null) {
                instance = new E03MoveAppearState();
            }
            return instance;
        }
    }
    #endregion

    private E03Transition[] transitions = { E03HasEndMoveToTransition.Instance };

    protected override Transition<E03Base>[] GetTransitions() {
        return transitions;
    }

    protected override void DoStartActions(StateController<E03Base> controller) {
        controller.ObjectBase.E03Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E03Base> controller) {
        //controller.ObjectBase.E03Move.MoveDirect();
        //controller.ObjectBase.E03Move.CheckingPositionAppearPoint();
    }

    protected override void DoEndActions(StateController<E03Base> controller) {
    }
}
