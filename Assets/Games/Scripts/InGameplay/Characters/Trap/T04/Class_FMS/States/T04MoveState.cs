

using Class_FSM;

public class T04MoveState : T04State {
    #region Singleton
    public T04MoveState() {

    }
    private static T04MoveState instance = null;
    public static T04MoveState Instance {
        get {
            if (instance == null) {
                instance = new T04MoveState();
            }
            return instance;
        }
    }
    #endregion

    private T04Transition[] transitions = { T04OutBoundTransition.Instance };
    protected override void DoEndActions(StateController<T04Base> controller) {
    }

    protected override void DoStartActions(StateController<T04Base> controller) {
        controller.ObjectBase.T04Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<T04Base> controller) {
        controller.ObjectBase.T04Move.MoveDirect();
    }

    protected override Transition<T04Base>[] GetTransitions() {
        return transitions;
    }
}
