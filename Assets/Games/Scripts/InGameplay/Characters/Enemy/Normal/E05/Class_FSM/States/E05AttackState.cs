

using Class_FSM;

public class E05AttackState : E05State {
    #region Singleton
    public E05AttackState() {

    }
    private static E05AttackState instance = null;
    public static E05AttackState Instance {
        get {
            if (instance == null) {
                instance = new E05AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E05Transition[] transitions = { E05HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E05Base> controller) {
        controller.ObjectBase.E05Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E05Base> controller) {
        controller.ObjectBase.E05Move.StartMoveIdle();
        controller.ObjectBase.E05Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E05Base> controller) {
        controller.ObjectBase.E05Attack.AimTarget();
    }

    protected override Transition<E05Base>[] GetTransitions() {
        return transitions;
    }
}
