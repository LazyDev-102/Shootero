using Class_FSM;
using UnityEngine;

public class B12MoveToAttack2State : B12State {
    #region Singleton
    public B12MoveToAttack2State() {

    }
    private static B12MoveToAttack2State instance = null;
    public static B12MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B12MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B12Transition[] transitions = { B12MoveToAttack2CompleteTransition.Instance, B12CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B12Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.MoveDirect();
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}