

using Class_FSM;

public class E16StartState : E16State {
    #region Singleton
    public E16StartState() {

    }
    private static E16StartState instance = null;
    public static E16StartState Instance {
        get {
            if(instance == null) {
                instance = new E16StartState();
            }
            return instance;
        }
    }
    #endregion
    private E16Transition[] transitions = { E16CanAppearTransition.Instance};
    protected override void DoEndActions(StateController<E16Base> controller) {
    }

    protected override void DoStartActions(StateController<E16Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E16Base> controller) {
    }

    protected override Transition<E16Base>[] GetTransitions() {
        return transitions;
    }
}
