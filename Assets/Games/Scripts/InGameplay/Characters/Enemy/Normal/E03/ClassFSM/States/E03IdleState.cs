

using Class_FSM;

public class E03IdleState : E03State {
    #region Singleton
    public E03IdleState() {

    }
    private static E03IdleState instance = null;
    public static E03IdleState Instance {
        get {
            if(instance == null) {
                instance = new E03IdleState();
            }
            return instance;
        }
    }
    #endregion

    private E03Transition[] transitions = { E03CanMoveAppearTransition.Instance };
    protected override void DoEndActions(StateController<E03Base> controller) {
    }

    protected override void DoStartActions(StateController<E03Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E03Base> controller) {
    }

    protected override Transition<E03Base>[] GetTransitions() {
        return transitions;
    }

}
