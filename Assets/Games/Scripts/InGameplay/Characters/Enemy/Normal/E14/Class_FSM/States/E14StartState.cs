

using Class_FSM;

public class E14StartState : E14State {
    #region Singleton
    public E14StartState() {

    }
    private static E14StartState instance = null;
    public static E14StartState Instance {
        get {
            if(instance == null) {
                instance = new E14StartState();
            }
            return instance;
        }
    }
    #endregion
    private E14Transition[] transitions = { E14CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E14Base> controller) {
    }

    protected override void DoStartActions(StateController<E14Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E14Base> controller) {
    }

    protected override Transition<E14Base>[] GetTransitions() {
        return transitions;
    }
}
