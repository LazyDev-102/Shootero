

using Class_FSM;

public class E04IdleState : E04State {
    #region Singleton
    public E04IdleState() {

    }
    private static E04IdleState instance = null;
    public static E04IdleState Instance {
        get {
            if (instance == null) {
                instance = new E04IdleState();
            }
            return instance;
        }
    }
    #endregion
    private E04Transition[] transitions = { E04CanMoveAttackTransition.Instance };
    protected override void DoEndActions(StateController<E04Base> controller) {
        controller.ObjectBase.E04Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E04Base> controller) {
        controller.ObjectBase.E04Move.StartRotateNormal();
        controller.ObjectBase.E04Attack.StartAimTarget();
        controller.ObjectBase.E04Move.StartMoveIdle();
    }

    protected override void DoUpdateActions(StateController<E04Base> controller) {
        controller.ObjectBase.E04Move.RotateSelf();
        controller.ObjectBase.E04Attack.AimTarget();
    }

    protected override Transition<E04Base>[] GetTransitions() {
        return transitions;
    }
}
