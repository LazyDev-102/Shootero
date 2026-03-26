

using Class_FSM;

public class T02StartState : T02State {
    #region Singleton
    public T02StartState() {

    }
    private static T02StartState instance = null;
    public static T02StartState Instance {
        get {
            if (instance == null) {
                instance = new T02StartState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<T02Base> controller) {
    }

    protected override void DoStartActions(StateController<T02Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<T02Base> controller) {
    }

    protected override Transition<T02Base>[] GetTransitions() {
        return null;
    }
}
