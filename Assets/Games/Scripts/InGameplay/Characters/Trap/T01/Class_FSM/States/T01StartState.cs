

using Class_FSM;

public class T01StartState : T01State {
    #region Singleton
    public T01StartState() {

    }
    private static T01StartState instance = null;
    public static T01StartState Instance {
        get {
            if (instance == null) {
                instance = new T01StartState();
            }
            return instance;
        }
    }
    #endregion

    private T01Transition[] transitions = { T01CanMoveTransition.Instance };
    protected override void DoEndActions(StateController<T01Base> controller) {
    }

    protected override void DoStartActions(StateController<T01Base> controller) {
        controller.ObjectBase.Spawn();
        controller.ObjectBase.SpawnParts();
    }

    protected override void DoUpdateActions(StateController<T01Base> controller) {
    }

    protected override Transition<T01Base>[] GetTransitions() {
        return transitions;
    }
}
