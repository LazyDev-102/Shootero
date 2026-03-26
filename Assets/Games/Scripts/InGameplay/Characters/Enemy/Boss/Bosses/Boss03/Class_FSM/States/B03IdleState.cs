

using Class_FSM;

public class B03IdleState : B03State {
    #region Singleton
    public B03IdleState() {

    }
    private static B03IdleState instance = null;
    public static B03IdleState Instance {
        get {
            if (instance == null) {
                instance = new B03IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B03Transition[] transitions = { B03CanRageTransition.Instance, B03CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<B03Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B03Base> controller) {

    }

    protected override void DoUpdateActions(StateController<B03Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<B03Base>[] GetTransitions() {
        return transitions;
    }
}
