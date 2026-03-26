

using Class_FSM;
using UnityEngine;

public class HB01AppearState : HB01State {
    #region Singleton
    public HB01AppearState() {

    }
    private static HB01AppearState instance = null;
    public static HB01AppearState Instance {
        get {
            if (instance == null) {
                instance = new HB01AppearState();
            }
            return instance;
        }
    }
    #endregion

    private HB01Transition[] transitions = { HB01AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<HB01Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
        controller.ObjectBase.HB01Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
