

using Class_FSM;

public class B04IdleState : B04State {

    #region Singleton
    public B04IdleState() {

    }
    private static B04IdleState instance = null;
    public static B04IdleState Instance {
        get {
            if (instance == null) {
                instance = new B04IdleState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04CanRageTransition.Instance, B04CanAttackTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<B04Base> controller) {

    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
        controller.ObjectBase.CountdownIdle();
        //controller.ObjectBase.B04Move.ClosingWing();
        controller.ObjectBase.LookTarget();
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
