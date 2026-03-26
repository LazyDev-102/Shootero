

using Class_FSM;

public class B07AttackState : B07State {
    #region Singleton
    public B07AttackState() {

    }
    private static B07AttackState instance = null;
    public static B07AttackState Instance {
        get {
            if (instance == null) {
                instance = new B07AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B07Transition[] transitions = { B07EndAttackTransition.Instance, B07CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B07Base> controller) {
    }

    protected override void DoStartActions(StateController<B07Base> controller) {
        controller.ObjectBase.B07Attack.ChooseAttack();
        controller.ObjectBase.B07Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B07Base> controller) {
    }

    protected override Transition<B07Base>[] GetTransitions() {
        return transitions;
    }
}
