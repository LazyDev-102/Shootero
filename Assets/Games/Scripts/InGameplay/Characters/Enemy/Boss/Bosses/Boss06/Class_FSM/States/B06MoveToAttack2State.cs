using Class_FSM;
using UnityEngine;

public class B06MoveToAttack2State : B06State {
    #region Singleton
    public B06MoveToAttack2State() {

    }
    private static B06MoveToAttack2State instance = null;
    public static B06MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B06MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B06Transition[] transitions = { B06MoveToAttack2CompleteTransition.Instance, B06CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B06Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Move.MoveDirect();
    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}