

using Class_FSM;

public class E10AttackState : E10State {
    #region Singleton
    public E10AttackState() {

    }
    private static E10AttackState instance = null;
    public static E10AttackState Instance {
        get {
            if (instance == null) {
                instance = new E10AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E10Transition[] transitions = { E10IsShotTransition.Instance };
    protected override void DoEndActions(StateController<E10Base> controller) {
        controller.ObjectBase.E10Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E10Base> controller) {
        controller.ObjectBase.E10Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E10Base> controller) {
        controller.ObjectBase.E10Attack.EndShotCountdown();
    }

    protected override Transition<E10Base>[] GetTransitions() {
        return transitions;
    }
}
