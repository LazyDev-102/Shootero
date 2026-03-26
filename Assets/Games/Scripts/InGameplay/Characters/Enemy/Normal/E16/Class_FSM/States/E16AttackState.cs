

using Class_FSM;

public class E16AttackState : E16State {
    #region Singleton
    public E16AttackState() {

    }
    private static E16AttackState instance = null;
    public static E16AttackState Instance {
        get {
            if (instance == null) {
                instance = new E16AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E16Transition[] transitions = { E16HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E16Base> controller) {
        controller.ObjectBase.E16Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E16Base> controller) {
        controller.ObjectBase.E16Move.StartMoveIdle();
        controller.ObjectBase.E16Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E16Base> controller) {

    }

    protected override Transition<E16Base>[] GetTransitions() {
        return transitions;
    }
}
