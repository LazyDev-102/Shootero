using Class_FSM;
using UnityEngine;

public class B14MoveToAttack2State : B14State {
    #region Singleton
    public B14MoveToAttack2State() {

    }
    private static B14MoveToAttack2State instance = null;
    public static B14MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B14MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B14Transition[] transitions = { B14MoveToAttack2CompleteTransition.Instance, B14CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B14Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Move.MoveDirect();
    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}