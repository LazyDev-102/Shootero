

using Class_FSM;

public class B02AttackRageState : B02State {
    #region Singleton
    public B02AttackRageState() {

    }
    private static B02AttackRageState instance = null;
    public static B02AttackRageState Instance {
        get {
            if (instance == null) {
                instance = new B02AttackRageState();
            }
            return instance;
        }
    }
    #endregion

    private B02Transition[] transitions = { B02EndAttackRageTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B02Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B02Base> controller) {
        controller.ObjectBase.B02Attack.StartRage();
        controller.ObjectBase.B02Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
