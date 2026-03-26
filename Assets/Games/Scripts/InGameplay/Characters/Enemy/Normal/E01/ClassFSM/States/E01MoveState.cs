using Class_FSM;

public class E01MoveState : E01State {
    #region Singleton
    public E01MoveState() {

    }
    private static E01MoveState instance = null;
    public static E01MoveState Instance {
        get {
            if (instance == null) {
                instance = new E01MoveState();
            }
            return instance;
        }
    }
    #endregion

    private Transition<E01Base>[] transitions = { E01OutBoundTransition.Instance };
    protected override void DoEndActions(StateController<E01Base> controller) {
    }

    protected override void DoStartActions(StateController<E01Base> controller) {
        // start appear
        controller.ObjectBase.E01Move.StartMoveAppear();
        // start rotate
        controller.ObjectBase.E01Skin.StartRotateSelf();
    }

    protected override void DoUpdateActions(StateController<E01Base> controller) {
        // move direction
        controller.ObjectBase.E01Move.MoveDirect();
        controller.ObjectBase.E01Move.CheckCurrentSpeed();
        // rotate
        controller.ObjectBase.E01Skin.RotateSelf();
    }

    protected override Transition<E01Base>[] GetTransitions() {
        return transitions;
    }
}
