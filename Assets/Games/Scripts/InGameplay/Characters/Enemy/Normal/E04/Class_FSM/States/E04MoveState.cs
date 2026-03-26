

using Class_FSM;

public class E04MoveState : E04State {
    #region Singleton
    public E04MoveState() {

    }
    private static E04MoveState instance = null;
    public static E04MoveState Instance {
        get {
            if(instance == null) {
                instance = new E04MoveState();
            }
            return instance;
        }
    }
    #endregion
    private E04Transition[] transitions = { E04HasMoveEndTransition.Instance };
    protected override void DoEndActions(StateController<E04Base> controller) {
        controller.ObjectBase.E04Move.HideMoveTrail();
        controller.ObjectBase.E04Attack.EndAttack();
    }

    protected override void DoStartActions(StateController<E04Base> controller) {
        controller.ObjectBase.E04Attack.Attack();
        controller.ObjectBase.E04Move.StartRotateAttack();
    }

    protected override void DoUpdateActions(StateController<E04Base> controller) {
        E04Move move = controller.ObjectBase.E04Move;
        move.RotateSelf();
        move.MoveDirect();
    }

    protected override Transition<E04Base>[] GetTransitions() {
        return transitions;
    }
}
