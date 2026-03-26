

using Class_FSM;

public class ME02B08AttackState : ME02B08State {
    #region Singleton
    public ME02B08AttackState() {

    }
    private static ME02B08AttackState instance = null;
    public static ME02B08AttackState Instance {
        get {
            if (instance == null) {
                instance = new ME02B08AttackState();
            }
            return instance;
        }
    }
    #endregion

    protected override void DoEndActions(StateController<ME02B08Base> controller) {
    }

    protected override void DoStartActions(StateController<ME02B08Base> controller) {
        controller.ObjectBase.ME02B08Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<ME02B08Base> controller) {
        controller.ObjectBase.ME02B08Attack.BeamingLaser();
        controller.ObjectBase.ME02B08Move.RotatingSefl();
    }

    protected override Transition<ME02B08Base>[] GetTransitions() {
        return null;
    }
}
