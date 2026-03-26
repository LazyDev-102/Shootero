

using Class_FSM;

public class ME03B10AttackState : ME03B10State {

    #region Singleton
    public ME03B10AttackState() {

    }
    private static ME03B10AttackState instance = null;
    public static ME03B10AttackState Instance {
        get {
            if (instance == null) {
                instance = new ME03B10AttackState();
            }
            return instance;
        }
    }
    #endregion

    private ME03B10Transition[] transitions = { ME03B10EndAttackTransition.Instance };
    protected override void DoEndActions(StateController<ME03B10Base> controller) {
        controller.ObjectBase.ME03B10Attack.EndAttack();
    }

    protected override void DoStartActions(StateController<ME03B10Base> controller) {
        controller.ObjectBase.ME03B10Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<ME03B10Base> controller) {
        controller.ObjectBase.ME03B10Attack.UpdateLine();
    }

    protected override Transition<ME03B10Base>[] GetTransitions() {
        return transitions;
    }
}
