

using Class_FSM;

public class E08AttackState : E08State {
    #region Singleton
    public E08AttackState() {

    }
    private static E08AttackState instance = null;
    public static E08AttackState Instance {
        get {
            if(instance == null) {
                instance = new E08AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E08Transition[] transitions = { E08EndAttackTransition.Instance };
    protected override void DoEndActions(StateController<E08Base> controller) {
        controller.ObjectBase.E08Attack.EndAttack();
    }

    protected override void DoStartActions(StateController<E08Base> controller) {
        controller.ObjectBase.E08Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E08Base> controller) {
        controller.ObjectBase.E08Attack.BeamingLaser();
    }

    protected override Transition<E08Base>[] GetTransitions() {
        return transitions;
    }
}
