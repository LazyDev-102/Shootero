

using Class_FSM;

public class T01MoveState : T01State {
    #region Singleton
    public T01MoveState() {

    }
    private static T01MoveState instance = null;
    public static T01MoveState Instance {
        get {
            if (instance == null) {
                instance = new T01MoveState();
            }
            return instance;
        }
    }
    #endregion

    private T01Transition[] transitons = { T01OutBoundTransition.Instance };
    protected override void DoEndActions(StateController<T01Base> controller) {
    }

    protected override void DoStartActions(StateController<T01Base> controller) {
        controller.ObjectBase.T01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<T01Base> controller) {
        controller.ObjectBase.T01Move.MoveDirect();
    }

    protected override Transition<T01Base>[] GetTransitions() {
        return transitons;
    }
}
