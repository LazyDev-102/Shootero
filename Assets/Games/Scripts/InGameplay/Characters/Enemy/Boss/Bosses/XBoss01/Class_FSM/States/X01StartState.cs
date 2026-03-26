

using Class_FSM;
using UnityEngine;

public class XB01StartState : XB01State {
    #region Singleton
    public XB01StartState() {

    }
    private static XB01StartState instance = null;
    public static XB01StartState Instance {
        get {
            if (instance == null) {
                instance = new XB01StartState();
            }
            return instance;
        }
    }
    #endregion
    private XB01Transition[] transitions = { XB01CanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<XB01Base> controller) {
    }

    protected override void DoStartActions(StateController<XB01Base> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<XB01Base> controller) {
    }

    protected override Transition<XB01Base>[] GetTransitions() {
        return transitions;
    }
}
