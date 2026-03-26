

using Class_FSM;

public class E12StartState : E12State {
    #region Singleton
    public E12StartState() {

    }
    private static E12StartState instance = null;
    public static E12StartState Instance {
        get {
            if(instance == null) {
                instance = new E12StartState();
            }
            return instance;
        }
    }
    #endregion
    private E12Transition[] transitions = { E12CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E12Base> controller) {
    }

    protected override void DoStartActions(StateController<E12Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E12Base> controller) {
    }

    protected override Transition<E12Base>[] GetTransitions() {
        return transitions;
    }
}
