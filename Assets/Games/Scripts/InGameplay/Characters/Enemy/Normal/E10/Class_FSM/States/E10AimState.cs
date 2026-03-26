

using Class_FSM;

public class E10AimState : E10State {
    #region Singleton
    public E10AimState() {

    }
    private static E10AimState instance = null;
    public static E10AimState Instance {
        get {
            if (instance == null) {
                instance = new E10AimState();
            }
            return instance;
        }
    }
    #endregion

    private E10Transition[] transitions = { E10CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<E10Base> controller) {

    }

    protected override void DoStartActions(StateController<E10Base> controller) {
        controller.ObjectBase.E10Move.StartMoveIdle();
        controller.ObjectBase.E10Attack.StartAimTarget();
    }

    protected override void DoUpdateActions(StateController<E10Base> controller) {
        controller.ObjectBase.E10Attack.AimTarget();
    }

    protected override Transition<E10Base>[] GetTransitions() {
        return transitions;
    }
}
