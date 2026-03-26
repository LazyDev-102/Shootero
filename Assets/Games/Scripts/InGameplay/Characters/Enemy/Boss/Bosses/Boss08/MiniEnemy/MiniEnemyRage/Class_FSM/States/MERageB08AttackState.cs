

using Class_FSM;

public class MERageB08AttackState : MERageB08State {
    #region Singleton
    public MERageB08AttackState() {

    }
    private static MERageB08AttackState instance = null;
    public static MERageB08AttackState Instance {
        get {
            if (instance == null) {
                instance = new MERageB08AttackState();
            }
            return instance;
        }
    }
    #endregion
    protected override void DoEndActions(StateController<MERageB08Base> controller) {
    }

    protected override void DoStartActions(StateController<MERageB08Base> controller) {
        controller.ObjectBase.MERageB08Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<MERageB08Base> controller) {
        controller.ObjectBase.MERageB08Attack.Healing();
    }

    protected override Transition<MERageB08Base>[] GetTransitions() {
        return null;
    }
}
