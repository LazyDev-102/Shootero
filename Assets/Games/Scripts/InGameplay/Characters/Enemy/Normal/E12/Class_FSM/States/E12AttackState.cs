

using Class_FSM;

public class E12AttackState : E12State {
    #region Singleton
    public E12AttackState() {

    }
    private static E12AttackState instance = null;
    public static E12AttackState Instance {
        get {
            if (instance == null) {
                instance = new E12AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E12Transition[] transitions = { E12HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E12Base> controller) {
        controller.ObjectBase.E12Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E12Base> controller) {
        controller.ObjectBase.E12Move.StartMoveIdle();
        controller.ObjectBase.E12Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E12Base> controller) {
    }

    protected override Transition<E12Base>[] GetTransitions() {
        return transitions;
    }
}
