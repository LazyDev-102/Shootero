

using Class_FSM;

public class E03AttackState : E03State {
    #region Singleton
    public E03AttackState() {

    }
    private static E03AttackState instance = null;
    public static E03AttackState Instance {
        get {
            if (instance == null) {
                instance = new E03AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E03Transition[] transitions = { E03HasEndAttackTransition.Instance };
    protected override void DoEndActions(StateController<E03Base> controller) {
        controller.ObjectBase.E03Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E03Base> controller) {
        controller.ObjectBase.E03Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E03Base> controller) {
        controller.ObjectBase.E03Attack.AimTarget();
    }

    protected override Transition<E03Base>[] GetTransitions() {
        return transitions;
    }
}
