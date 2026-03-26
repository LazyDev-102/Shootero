

using Class_FSM;

public class E07StartState : E07State {
    #region Singleton
    public E07StartState() {

    }
    private static E07StartState instance = null;
    public static E07StartState Instance {
        get {
            if(instance == null) {
                instance = new E07StartState();
            }
            return instance;
        }
    }
    #endregion
    private E07Transition[] transitions = { E07CanAppearTransition.Instance };
    protected override void DoEndActions(StateController<E07Base> controller) {
    }

    protected override void DoStartActions(StateController<E07Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E07Base> controller) {
    }

    protected override Transition<E07Base>[] GetTransitions() {
        return transitions;
    }
}
