

using Class_FSM;

public class T04StartState : T04State {
    #region Singleton
    public T04StartState() {

    }
    private static T04StartState instance = null;
    public static T04StartState Instance {
        get {
            if (instance == null) {
                instance = new T04StartState();
            }
            return instance;
        }
    }
    #endregion

    private T04Transition[] transitons = { T04CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<T04Base> controller) {
    }

    protected override void DoStartActions(StateController<T04Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<T04Base> controller) {
    }

    protected override Transition<T04Base>[] GetTransitions() {
        return transitons;
    }
}
