

using Class_FSM;

public class E03AimState : E03State {

    #region Singleton
    public E03AimState() {

    }
    private static E03AimState instance = null;
    public static E03AimState Instance {
        get {
            if (instance == null) {
                instance = new E03AimState();
            }
            return instance;
        }
    }
    #endregion
    private E03Transition[] transitions = { E03CanAttackTransition.Instance };

    protected override void DoEndActions(StateController<E03Base> controller) {
    }

    protected override void DoStartActions(StateController<E03Base> controller) {
        controller.ObjectBase.E03Attack.StartAimTarget();
        controller.ObjectBase.E03Move.StartMoveIdle();
    }

    protected override void DoUpdateActions(StateController<E03Base> controller) {
        controller.ObjectBase.E03Attack.AimTarget();
    }

    protected override Transition<E03Base>[] GetTransitions() {
        return transitions;
    }
}
