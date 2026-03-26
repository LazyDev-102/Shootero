using Class_FSM;
using UnityEngine;

public class B11MoveToAttack2State : B11State {
    #region Singleton
    public B11MoveToAttack2State() {

    }
    private static B11MoveToAttack2State instance = null;
    public static B11MoveToAttack2State Instance {
        get {
            if (instance == null) {
                instance = new B11MoveToAttack2State();
            }
            return instance;
        }
    }
    #endregion
    private B11Transition[] transitions = { B11MoveToAttack2CompleteTransition.Instance, B11CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<B11Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.MoveDirect();
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}