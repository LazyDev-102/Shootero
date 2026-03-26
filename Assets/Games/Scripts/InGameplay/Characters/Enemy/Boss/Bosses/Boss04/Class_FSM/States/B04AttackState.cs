

using Class_FSM;

public class B04AttackState : B04State {
    #region Singleton
    public B04AttackState() {

    }
    private static B04AttackState instance = null;
    public static B04AttackState Instance {
        get {
            if (instance == null) {
                instance = new B04AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04EndAttackTransition.Instance, B04CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Attack.ChooseAttack();
        controller.ObjectBase.B04Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
