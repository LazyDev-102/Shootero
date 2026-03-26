

using Class_FSM;
using UnityEngine;

public class HB01MoveState : HB01State {
    #region Singleton
    public HB01MoveState() {

    }
    private static HB01MoveState instance = null;
    public static HB01MoveState Instance {
        get {
            if (instance == null) {
                instance = new HB01MoveState();
            }
            return instance;
        }
    }
    #endregion
    private HB01Transition[] transitions = { HB01MoveCompleteTransition.Instance, HB01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.blue;
    protected override void DoEndActions(StateController<HB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAttack();
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
        controller.ObjectBase.HB01Move.StartMoveAfterAttack();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.HB01Move.MoveDirect();
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
