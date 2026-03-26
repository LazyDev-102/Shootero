

using Class_FSM;

public class B02IdleState : B02State {
    #region Singleton
    public B02IdleState() {

    }
    private static B02IdleState instance = null;
    public static B02IdleState Instance {
        get {
            if (instance == null) {
                instance = new B02IdleState();
            }
            return instance;
        }
    }
    #endregion

    private B02Transition[] transitions = { B02CanRageTransition.Instance, B02CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<B02Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B02Base> controller) {

    }

    protected override void DoUpdateActions(StateController<B02Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B02Base>[] GetTransitions() {
        return transitions;
    }
}
