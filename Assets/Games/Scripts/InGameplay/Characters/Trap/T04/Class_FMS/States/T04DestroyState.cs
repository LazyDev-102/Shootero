

using Class_FSM;

public class T04DestroyState : T04State {
    #region Singleton
    public T04DestroyState() {

    }
    private static T04DestroyState instance = null;
    public static T04DestroyState Instance {
        get {
            if (instance == null) {
                instance = new T04DestroyState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<T04Base> controller) {
    }

    protected override void DoStartActions(StateController<T04Base> controller) {
        controller.ObjectBase.Despawn();
    }

    protected override void DoUpdateActions(StateController<T04Base> controller) {
    }

    protected override Transition<T04Base>[] GetTransitions() {
        return null;
    }
}
