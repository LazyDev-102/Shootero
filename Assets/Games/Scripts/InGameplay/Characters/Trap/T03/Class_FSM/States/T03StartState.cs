

using Class_FSM;

public class T03StartState : T03State {
    #region Singleton
    public T03StartState() {

    }
    private static T03StartState instance = null;
    public static T03StartState Instance {
        get {
            if (instance == null) {
                instance = new T03StartState();
            }
            return instance;
        }
    }
    #endregion

    private T03Transition[] transitons = { T03CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<T03Base> controller) {
    }

    protected override void DoStartActions(StateController<T03Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<T03Base> controller) {
    }

    protected override Transition<T03Base>[] GetTransitions() {
        return transitons;
    }
}
