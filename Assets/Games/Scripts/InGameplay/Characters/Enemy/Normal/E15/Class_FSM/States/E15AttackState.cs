

using Class_FSM;

public class E15AttackState : E15State {
    #region Singleton
    public E15AttackState() {

    }
    private static E15AttackState instance = null;
    public static E15AttackState Instance {
        get {
            if (instance == null) {
                instance = new E15AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E15Transition[] transitions = { E15HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E15Base> controller) {
    }

    protected override void DoStartActions(StateController<E15Base> controller) {
        controller.ObjectBase.E15Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E15Base> controller) {

    }

    protected override Transition<E15Base>[] GetTransitions() {
        return transitions;
    }
}
