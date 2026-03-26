

using Class_FSM;

public class E11AttackState : E11State {
    #region Singleton
    public E11AttackState() {

    }
    private static E11AttackState instance = null;
    public static E11AttackState Instance {
        get {
            if (instance == null) {
                instance = new E11AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E11Transition[] transitions = { E11HasAttackEndTransition.Instance };
    protected override void DoEndActions(StateController<E11Base> controller) {
        controller.ObjectBase.E11Move.EndMoveIdle();
    }

    protected override void DoStartActions(StateController<E11Base> controller) {
        controller.ObjectBase.E11Move.StartMoveIdle();
        controller.ObjectBase.E11Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E11Base> controller) {
        controller.ObjectBase.E11Attack.AimTarget();
    }

    protected override Transition<E11Base>[] GetTransitions() {
        return transitions;
    }
}
