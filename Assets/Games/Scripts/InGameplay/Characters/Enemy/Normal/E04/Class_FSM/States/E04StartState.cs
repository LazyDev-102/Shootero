

using Class_FSM;

public class E04StartState : E04State {
    #region Singleton
    public E04StartState() {

    }
    private static E04StartState instance = null;
    public static E04StartState Instance {
        get {
            if(instance == null) {
                instance = new E04StartState();
            }
            return instance;
        }
    }
    #endregion

    private E04Transition[] transitions = { E04CanAppearTransition.Instance }; 
    protected override void DoEndActions(StateController<E04Base> controller) {

    }

    protected override void DoStartActions(StateController<E04Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E04Base> controller) {

    }

    protected override Transition<E04Base>[] GetTransitions() {
        return transitions;
    }
}
