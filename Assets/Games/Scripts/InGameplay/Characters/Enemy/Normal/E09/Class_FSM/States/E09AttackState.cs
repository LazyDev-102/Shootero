

using Class_FSM;

public class E09AttackState : E09State {
    #region Singleton
    public E09AttackState() {

    }
    private static E09AttackState instance = null;
    public static E09AttackState Instance {
        get {
            if(instance == null) {
                instance = new E09AttackState();
            }
            return instance;
        }
    }
    #endregion
    private E09Transition[] transitions = { E09EndAttackTransition.Instance };
    protected override void DoEndActions(StateController<E09Base> controller) {
        controller.ObjectBase.E09Attack.EndAttack();
    }

    protected override void DoStartActions(StateController<E09Base> controller) {
        controller.ObjectBase.E09Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<E09Base> controller) {
        controller.ObjectBase.E09Attack.BeamingLaser();
    }

    protected override Transition<E09Base>[] GetTransitions() {
        return transitions;
    }
}
