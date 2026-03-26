

using Class_FSM;

public class T03MoveState : T03State {
    #region Singleton
    public T03MoveState() {

    }
    private static T03MoveState instance = null;
    public static T03MoveState Instance {
        get {
            if (instance == null) {
                instance = new T03MoveState();
            }
            return instance;
        }
    }
    #endregion

    private T03Transition[] transitions = { T03OutBoundTransition.Instance };
    protected override void DoEndActions(StateController<T03Base> controller) {
    }

    protected override void DoStartActions(StateController<T03Base> controller) {
        controller.ObjectBase.T03Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<T03Base> controller) {
        controller.ObjectBase.T03Move.MoveDirect();
    }

    protected override Transition<T03Base>[] GetTransitions() {
        return transitions;
    }
}
