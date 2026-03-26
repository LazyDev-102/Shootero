

using Class_FSM;

public class T01DestroyState : T01State {
    #region Singleton
    public T01DestroyState() {

    }
    private static T01DestroyState instance = null;
    public static T01DestroyState Instance {
        get {
            if (instance == null) {
                instance = new T01DestroyState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<T01Base> controller) {
    }

    protected override void DoStartActions(StateController<T01Base> controller) {
        controller.ObjectBase.Despawn();
    }

    protected override void DoUpdateActions(StateController<T01Base> controller) {
    }

    protected override Transition<T01Base>[] GetTransitions() {
        return null;
    }
}
