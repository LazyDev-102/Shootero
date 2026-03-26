

using Class_FSM;

public class E02MoveState : E02State {
    #region Singleton
    public E02MoveState() {

    }
    private static E02MoveState instance = null;
    public static E02MoveState Instance {
        get {
            if (instance == null) {
                instance = new E02MoveState();
            }
            return instance;
        }
    }
    #endregion

    private E02Transition[] transitions = { E02HasOutBoundTransiton.Instance, E02HasCompleteKnockTransition.Instance };
    protected override void DoEndActions(StateController<E02Base> controller) {
        controller.ObjectBase.E02Move.HideMoveTrail();
        controller.ObjectBase.E02Move.EndTargetMoveAttack();
    }

    protected override void DoStartActions(StateController<E02Base> controller) {
    }

    protected override void DoUpdateActions(StateController<E02Base> controller) {
        controller.ObjectBase.E02Move.MoveDirect();
    }

    protected override Transition<E02Base>[] GetTransitions() {
        return transitions;
    }
}
