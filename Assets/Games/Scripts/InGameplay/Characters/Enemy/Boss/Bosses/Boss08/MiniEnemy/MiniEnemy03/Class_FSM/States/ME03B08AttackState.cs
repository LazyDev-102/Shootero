

using Class_FSM;

public class ME03B08AttackState : ME03B08State {
    #region Singleton
    public ME03B08AttackState() {

    }
    private static ME03B08AttackState instance = null;
    public static ME03B08AttackState Instance {
        get {
            if (instance == null) {
                instance = new ME03B08AttackState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<ME03B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME03B08Base> controller) {
        controller.ObjectBase.ME03B08Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<ME03B08Base> controller) {
        controller.ObjectBase.ME03B08Attack.BeamingLaser();
    }

    protected override Transition<ME03B08Base>[] GetTransitions() {
        return null;
    }
}
