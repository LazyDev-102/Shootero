

using Class_FSM;

public class E06StartState : E06State {
    #region Singleton
    public E06StartState() {

    }
    private static E06StartState instance = null;
    public static E06StartState Instance {
        get {
            if(instance == null) {
                instance = new E06StartState();
            }
            return instance;
        }
    }
    #endregion
    private E06Transition[] transitions = { E06CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E06Base> controller) {
    }

    protected override void DoStartActions(StateController<E06Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E06Base> controller) {
    }

    protected override Transition<E06Base>[] GetTransitions() {
        return transitions;
    }
}
