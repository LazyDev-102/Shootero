

using Class_FSM;

public class B10AttackState : B10State {
    #region Singleton
    public B10AttackState() {

    }
    private static B10AttackState instance = null;
    public static B10AttackState Instance {
        get {
            if (instance == null) {
                instance = new B10AttackState();
            }
            return instance;
        }
    }
    #endregion

    private B10Transition[] transitons = { B10EndAttackTransition.Instance, B10CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B10Base> controller) {
    }

    protected override void DoStartActions(StateController<B10Base> controller) {
        controller.ObjectBase.B10Attack.ChooseAttack();
        controller.ObjectBase.B10Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B10Base> controller) {
    }

    protected override Transition<B10Base>[] GetTransitions() {
        return transitons;
    }
}
