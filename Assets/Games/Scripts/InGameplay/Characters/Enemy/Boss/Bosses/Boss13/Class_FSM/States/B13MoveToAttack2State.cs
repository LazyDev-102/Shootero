using Class_FSM;
using UnityEngine;

public class B13MoveToAttack2State : B13State {
    #region Singleton
    public B13MoveToAttack2State() {

    }
    private static B13MoveToAttack2State instance = null;
    public static B13MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B13MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B13Transition[] transitions = { B13MoveToAttack2CompleteTransition.Instance, B13CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B13Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.MoveDirect();
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
