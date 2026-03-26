

using Class_FSM;

public class E11StartState : E11State {
    #region Singleton
    public E11StartState() {

    }
    private static E11StartState instance = null;
    public static E11StartState Instance {
        get {
            if(instance == null) {
                instance = new E11StartState();
            }
            return instance;
        }
    }
    #endregion
    private E11Transition[] transitions = { E11CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E11Base> controller) {
    }

    protected override void DoStartActions(StateController<E11Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E11Base> controller) {
    }

    protected override Transition<E11Base>[] GetTransitions() {
        return transitions;
    }
}
