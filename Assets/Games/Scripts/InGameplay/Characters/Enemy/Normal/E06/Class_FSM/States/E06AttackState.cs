

using Class_FSM;

public class E06AttackState : E06State {
    #region Singleton
    public E06AttackState() {

    }
    private static E06AttackState instance = null;
    public static E06AttackState Instance {
        get {
            if (instance == null) {
                instance = new E06AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E06Transition[] transitions = { E06HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E06Base> controller) {
        controller.ObjectBase.E06Move.EndMoveIdle();

    }

    protected override void DoStartActions(StateController<E06Base> controller) {
        controller.ObjectBase.E06Move.StartMoveIdle();
        controller.ObjectBase.E06Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E06Base> controller) {
        controller.ObjectBase.E06Attack.AimTarget();
    }

    protected override Transition<E06Base>[] GetTransitions() {
        return transitions;
    }
}
