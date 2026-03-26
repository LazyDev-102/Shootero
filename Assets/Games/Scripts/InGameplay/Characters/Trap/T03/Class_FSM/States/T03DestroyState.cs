

using Class_FSM;

public class T03DestroyState : T03State {
    #region Singleton
    public T03DestroyState() {

    }
    private static T03DestroyState instance = null;
    public static T03DestroyState Instance {
        get {
            if (instance == null) {
                instance = new T03DestroyState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<T03Base> controller) {
    }

    protected override void DoStartActions(StateController<T03Base> controller) {
        controller.ObjectBase.Despawn();
    }

    protected override void DoUpdateActions(StateController<T03Base> controller) {
    }

    protected override Transition<T03Base>[] GetTransitions() {
        return null;
    }
}
