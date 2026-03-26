


using Class_FSM;

public class E07AttackMoveState : E07State {
    #region Singleton
    public E07AttackMoveState() {

    }
    private static E07AttackMoveState instance = null;
    public static E07AttackMoveState Instance {
        get {
            if (instance == null) {
                instance = new E07AttackMoveState();
            }
            return instance;
        }
    }
    #endregion
    private E07Transition[] transitions = { E07EndAttackTransition.Instance, E07HasCompleteKnockTransition.Instance };
    protected override void DoEndActions(StateController<E07Base> controller) {
        controller.ObjectBase.E07Move.HideMoveTrail();
        controller.ObjectBase.E07Attack.EndAttack();
        controller.ObjectBase.E07Move.EndTargetMoveAttack();
    }

    protected override void DoStartActions(StateController<E07Base> controller) {
        controller.ObjectBase.E07Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E07Base> controller) {
        //controller.ObjectBase.E07Attack.RadiatingCircle();
        //controller.ObjectBase.E07Move.MoveDirect();
    }

    protected override Transition<E07Base>[] GetTransitions() {
        return transitions;
    }
}
