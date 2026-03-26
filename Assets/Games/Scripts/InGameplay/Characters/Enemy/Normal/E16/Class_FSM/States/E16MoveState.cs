

using Class_FSM;

public class E16MoveState : E16State {
    #region Singleton
    public E16MoveState() {

    }
    private static E16MoveState instance = null;
    public static E16MoveState Instance {
        get {
            if (instance == null) {
                instance = new E16MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E16Transition[] transitions = { E16HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E16Base> controller) {
    }

    protected override void DoStartActions(StateController<E16Base> controller) {
        controller.ObjectBase.E16Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<E16Base> controller) {
    }

    protected override Transition<E16Base>[] GetTransitions() {
        return transitions;
    }
}
