

using Class_FSM;

public class E02MoveAppearState : E02State {
    #region Singleton
    public E02MoveAppearState() {

    }
    private static E02MoveAppearState instance = null;
    public static E02MoveAppearState Instance {
        get {
            if (instance == null) {
                instance = new E02MoveAppearState();
            }
            return instance;
        }
    }
    #endregion

    private E02Transition[] transitions = { E02HasCompleteAppearTranstion.Instance, E02HasCompleteKnockTransition.Instance };
    protected override void DoEndActions(StateController<E02Base> controller) {
    }

    protected override void DoStartActions(StateController<E02Base> controller) {
        controller.ObjectBase.E02Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E02Base> controller) {
        //controller.ObjectBase.E02Move.MoveDirect();
        //controller.ObjectBase.E02Move.CheckingPositionAppearPoint();
    }

    protected override Transition<E02Base>[] GetTransitions() {
        return transitions;
    }
}
