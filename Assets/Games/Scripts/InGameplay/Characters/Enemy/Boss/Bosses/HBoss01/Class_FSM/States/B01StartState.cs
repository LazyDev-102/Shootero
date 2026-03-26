

using Class_FSM;
using UnityEngine;

public class HB01StartState : HB01State {
    #region Singleton
    public HB01StartState() {

    }
    private static HB01StartState instance = null;
    public static HB01StartState Instance {
        get {
            if (instance == null) {
                instance = new HB01StartState();
            }
            return instance;
        }
    }
    #endregion
    private HB01Transition[] transitions = { HB01CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<HB01Base> controller) {
    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {
    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
