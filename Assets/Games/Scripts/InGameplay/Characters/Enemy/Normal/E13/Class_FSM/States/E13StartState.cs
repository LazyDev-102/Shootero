

using Class_FSM;

public class E13StartState : E13State {
    #region Singleton
    public E13StartState() {

    }
    private static E13StartState instance = null;
    public static E13StartState Instance {
        get {
            if(instance == null) {
                instance = new E13StartState();
            }
            return instance;
        }
    }
    #endregion
    private E13Transition[] transitions = { E13CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E13Base> controller) {
    }

    protected override void DoStartActions(StateController<E13Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E13Base> controller) {
    }

    protected override Transition<E13Base>[] GetTransitions() {
        return transitions;
    }
}
