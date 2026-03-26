

using Class_FSM;

public class B12ChildAttackState : B12ChildState {
    #region Singleton
    public B12ChildAttackState() {

    }
    private static B12ChildAttackState instance = null;
    public static B12ChildAttackState Instance {
        get {
            if (instance == null) {
                instance = new B12ChildAttackState();
            }
            return instance;
        }
    }
    #endregion

    private B12ChildTransition[] transitions = { B12ChildCanMoveAttackingTransition.Instance };

    protected override void DoEndActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.B12ChildMove.EndMoveIdle();
        controller.ObjectBase.B12ChildAttack.Attack();
    }

    protected override void DoStartActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.B12ChildAttack.StartAimTarget();
        controller.ObjectBase.B12ChildMove.StartMoveIdle();
    }

    protected override void DoUpdateActions(StateController<B12ChildBase> controller) {
        controller.ObjectBase.B12ChildAttack.AimTarget();
    }

    protected override Transition<B12ChildBase>[] GetTransitions() {
        return transitions;
    }
}
