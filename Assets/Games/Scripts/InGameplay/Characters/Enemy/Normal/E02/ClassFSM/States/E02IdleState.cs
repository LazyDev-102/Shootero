

using Class_FSM;

public class E02IdleState : E02State{
    #region Singleton
    public E02IdleState() {

    }
    private static E02IdleState instance = null;
    public static E02IdleState Instance {
        get {
            if(instance == null) {
                instance = new E02IdleState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<E02Base>[] transitons = { E02CanMoveAppearTransition.Instance };

    protected override Transition<E02Base>[] GetTransitions() {
        return transitons;
    }

    protected override void DoStartActions(StateController<E02Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<E02Base> controller) {
    }

    protected override void DoEndActions(StateController<E02Base> controller) {
    }


}
