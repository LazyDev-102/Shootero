

using Class_FSM;

public class B04AttackRageState : B04State {
    #region Singleton
    public B04AttackRageState() {

    }
    private static B04AttackRageState instance = null;
    public static B04AttackRageState Instance {
        get {
            if (instance == null) {
                instance = new B04AttackRageState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04EndAttackRageTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
        controller.ObjectBase.EndRage();
        controller.ObjectBase.B04Attack.EndRage();
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Attack.StartRage();
        controller.ObjectBase.B04Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
