

using Class_FSM;

public class MESpecialB08AttackState : MESpecialB08State {
    #region Singleton
    public MESpecialB08AttackState() {

    }
    private static MESpecialB08AttackState instance = null;
    public static MESpecialB08AttackState Instance {
        get {
            if (instance == null) {
                instance = new MESpecialB08AttackState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<MESpecialB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MESpecialB08Base> controller) {
        controller.ObjectBase.MESpecialB08Attack.Attack();
        controller.ObjectBase.Die();
    }

    protected override void DoUpdateActions(StateController<MESpecialB08Base> controller) {
    }

    protected override Transition<MESpecialB08Base>[] GetTransitions() {
        return null;
    }
}
