

using Class_FSM;

public class E14AttackState : E14State {
    #region Singleton
    public E14AttackState() {

    }
    private static E14AttackState instance = null;
    public static E14AttackState Instance {
        get {
            if (instance == null) {
                instance = new E14AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E14Transition[] transitions = { E14HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E14Base> controller) {
        controller.ObjectBase.E14Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E14Base> controller) {
        controller.ObjectBase.E14Move.StartMoveIdle();
        controller.ObjectBase.E14Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E14Base> controller) {

    }

    protected override Transition<E14Base>[] GetTransitions() {
        return transitions;
    }
}
