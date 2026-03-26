

using Class_FSM;

public class E05StartState : E05State {
    #region Singleton
    public E05StartState() {

    }
    private static E05StartState instance = null;
    public static E05StartState Instance {
        get {
            if(instance == null) {
                instance = new E05StartState();
            }
            return instance;
        }
    }
    #endregion
    private E05Transition[] transitions = { E05CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E05Base> controller) {
    }

    protected override void DoStartActions(StateController<E05Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E05Base> controller) {
    }

    protected override Transition<E05Base>[] GetTransitions() {
        return transitions;
    }
}
