

using Class_FSM;

public class E07AimState : E07State {
    #region Singleton
    public E07AimState() {

    }
    private static E07AimState instance = null;
    public static E07AimState Instance {
        get {
            if (instance == null) {
                instance = new E07AimState();
            }
            return instance;
        }
    }
    #endregion
    private E07Transition[] transitions = { E07CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<E07Base> controller) {
        controller.ObjectBase.E07Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E07Base> controller) {
        controller.ObjectBase.E07Attack.EndAttack();
        controller.ObjectBase.E07Attack.StartAimTarget();
        controller.ObjectBase.E07Move.StartMoveIdle();
    }

    protected override void DoUpdateActions(StateController<E07Base> controller) {
        controller.ObjectBase.E07Attack.AimTarget();
    }

    protected override Transition<E07Base>[] GetTransitions() {
        return transitions;
    }
}
