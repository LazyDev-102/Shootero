

using Class_FSM;

public class B03AttackRageState : B03State {
    #region Singleton
    public B03AttackRageState() {

    }
    private static B03AttackRageState instance = null;
    public static B03AttackRageState Instance {
        get {
            if (instance == null) {
                instance = new B03AttackRageState();
            }
            return instance;
        }
    }
    #endregion

    private B03Transition[] transitions = { B03EndAttackRageTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B03Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B03Base> controller) {
        controller.ObjectBase.B03Attack.StartRage();
        controller.ObjectBase.RestoreAllShield1();
        controller.ObjectBase.B03Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
