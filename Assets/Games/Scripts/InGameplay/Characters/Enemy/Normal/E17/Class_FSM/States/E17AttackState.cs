

using Class_FSM;

public class E17AttackState : E17State {
    #region Singleton
    public E17AttackState() {

    }
    private static E17AttackState instance = null;
    public static E17AttackState Instance {
        get {
            if (instance == null) {
                instance = new E17AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E17Transition[] transitions = { E17HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E17Base> controller) {
        controller.ObjectBase.E17Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E17Base> controller) {
        controller.ObjectBase.E17Move.StartMoveIdle();
        controller.ObjectBase.E17Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E17Base> controller) {
        controller.ObjectBase.E17Attack.Aim();
    }

    protected override Transition<E17Base>[] GetTransitions() {
        return transitions;
    }
}
