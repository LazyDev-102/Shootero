

using Class_FSM;
using UnityEngine;

public class HB01IdleState : HB01State {
    #region Singleton
    public HB01IdleState() {

    }
    private static HB01IdleState instance = null;
    public static HB01IdleState Instance {
        get {
            if (instance == null) {
                instance = new HB01IdleState();
            }
            return instance;
        }
    }
    #endregion
    private HB01Transition[] transitions = { HB01CanAttackTransition.Instance, HB01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.green;
    protected override void DoEndActions(StateController<HB01Base> controller) {
        // controller.ObjectBase.HB01Move.EndMoveIdle();
        controller.ObjectBase.EndIdle();
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
        controller.ObjectBase.LookTarget();
        controller.ObjectBase.CountdownIdle();
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
