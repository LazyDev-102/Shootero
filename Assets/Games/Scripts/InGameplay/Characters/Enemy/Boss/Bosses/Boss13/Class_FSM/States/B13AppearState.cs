

using Class_FSM;
using UnityEngine;

public class B13AppearState : B13State {
    #region Singleton
    public B13AppearState() {

    }
    private static B13AppearState instance = null;
    public static B13AppearState Instance {
        get {
            if (instance == null) {
                instance = new B13AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B13Transition[] transitions = { B13AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B13Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Move.MoveDirect();
    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
