

using Class_FSM;

public class B04MoveState : B04State {
    #region Singleton
    public B04MoveState() {

    }
    private static B04MoveState instance = null;
    public static B04MoveState Instance {
        get {
            if (instance == null) {
                instance = new B04MoveState();
            }
            return instance;
        }
    }
    #endregion
    private B04Transition[] transitions = { B04EndMoveTransition.Instance, B04CanRageTransition.Instance };
    protected override void DoEndActions(StateController<B04Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B04Base> controller) {
        controller.ObjectBase.B04Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B04Base> controller) {
        //controller.ObjectBase.B04Move.MoveDirectWithWing();
    }

    protected override Transition<B04Base>[] GetTransitions() {
        return transitions;
    }
}
