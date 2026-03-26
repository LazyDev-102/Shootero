using Class_FSM;
using UnityEngine;

public class B05MoveToAttack2State : B05State {
    #region Singleton
    public B05MoveToAttack2State() {

    }
    private static B05MoveToAttack2State instance = null;
    public static B05MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B05MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B05Transition[] transitions = { B05MoveToAttack2CompleteTransition.Instance, B05CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B05Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.MoveDirect();
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}