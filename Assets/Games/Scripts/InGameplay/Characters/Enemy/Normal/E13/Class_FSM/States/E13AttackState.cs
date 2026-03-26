

using Class_FSM;

public class E13AttackState : E13State {
    #region Singleton
    public E13AttackState() {

    }
    private static E13AttackState instance = null;
    public static E13AttackState Instance {
        get {
            if (instance == null) {
                instance = new E13AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E13Transition[] transitions = { E13HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E13Base> controller) {
        controller.ObjectBase.E13Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E13Base> controller) {
        controller.ObjectBase.E13Move.StartMoveIdle();
        controller.ObjectBase.E13Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E13Base> controller) {

    }

    protected override Transition<E13Base>[] GetTransitions() {
        return transitions;
    }
}
