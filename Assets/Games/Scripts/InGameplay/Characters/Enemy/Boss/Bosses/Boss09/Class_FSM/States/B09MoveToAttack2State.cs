using Class_FSM;
using UnityEngine;

public class B09MoveToAttack2State : B09State {
    #region Singleton
    public B09MoveToAttack2State() {

    }
    private static B09MoveToAttack2State instance = null;
    public static B09MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B09MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B09Transition[] transitions = { B09MoveToAttack2CompleteTransition.Instance, B09CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B09Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.MoveDirect();
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}