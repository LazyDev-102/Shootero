

using Class_FSM;

public class E02AttackState : E02State {
    #region Singleton
    public E02AttackState() {

    }
    private static E02AttackState instance = null;
    public static E02AttackState Instance {
        get {
            if (instance == null) {
                instance = new E02AttackState();
            }
            return instance;
        }
    }
    #endregion

    private E02Transition[] transitions = { E02CanMoveAttackingTransition.Instance };

    protected override void DoEndActions(StateController<E02Base> controller) {
        controller.ObjectBase.E02Move.EndMoveIdle();
        controller.ObjectBase.E02Attack.Attack();
    }

    protected override void DoStartActions(StateController<E02Base> controller) {
        controller.ObjectBase.E02Attack.StartAimTarget();
        controller.ObjectBase.E02Move.StartMoveIdle();
    }

    protected override void DoUpdateActions(StateController<E02Base> controller) {
        controller.ObjectBase.E02Attack.AimTarget();
    }

    protected override Transition<E02Base>[] GetTransitions() {
        return transitions;
    }
}
