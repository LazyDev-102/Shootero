

using Class_FSM;

public class B02AttackState : B02State {
    #region Singleton
    public B02AttackState() {

    }
    private static B02AttackState instance = null;
    public static B02AttackState Instance {
        get {
            if (instance == null) {
                instance = new B02AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B02Transition[] transitions = { B02EndAttackTransition.Instance, B02CanRageTransition.Instance };

    protected override void DoEndActions(StateController<B02Base> controller) {
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        B02Attack attack = controller.ObjectBase.B02Attack;
        attack.ChooseAttack();
        attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
