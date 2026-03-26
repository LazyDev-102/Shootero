

using Class_FSM;

public class B03AttackState : B03State {
    #region Singleton
    public B03AttackState() {

    }
    private static B03AttackState instance = null;
    public static B03AttackState Instance {
        get {
            if (instance == null) {
                instance = new B03AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03EndAttackTransition.Instance, B03CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        B03Attack attack = controller.ObjectBase.B03Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
