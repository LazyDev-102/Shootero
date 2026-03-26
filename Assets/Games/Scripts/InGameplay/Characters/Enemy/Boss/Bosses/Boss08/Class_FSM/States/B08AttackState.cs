

using Class_FSM;

public class B08AttackState : B08State {
    #region Singleton
    public B08AttackState() {

    }
    private static B08AttackState instance = null;
    public static B08AttackState Instance {
        get {
            if (instance == null) {
                instance = new B08AttackState();
            }
            return instance;
        }
    }
    #endregion

    private B08Transition[] transitions = { B08EndAttackTransition.Instance, B08CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B08Base> controller) {

    }

    protected override void DoStartActions(StateController<B08Base> controller) {
        controller.ObjectBase.B08Attack.ChooseAttack();
        controller.ObjectBase.B08Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B08Base> controller) {

    }

    protected override Transition<B08Base>[] GetTransitions() {
        return transitions;

    }
}
