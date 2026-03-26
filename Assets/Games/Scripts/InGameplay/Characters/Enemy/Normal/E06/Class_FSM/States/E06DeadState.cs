

using Class_FSM;

public class E06DeadState : E06State {
    #region Singleton
    public E06DeadState() {

    }
    private static E06DeadState instance = null;
    public static E06DeadState Instance {
        get {
            if(instance == null) {
                instance = new E06DeadState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<E06Base> controller) {
    }

    protected override void DoStartActions(StateController<E06Base> controller) {
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<E06Base> controller) {
    }

    protected override Transition<E06Base>[] GetTransitions() {
        return null;
    }
}
