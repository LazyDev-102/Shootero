

using Class_FSM;
using UnityEngine;

public class B12AppearState : B12State {
    #region Singleton
    public B12AppearState() {

    }
    private static B12AppearState instance = null;
    public static B12AppearState Instance {
        get {
            if(instance == null) {
                instance = new B12AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B12Transition[] transitions = { B12AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B12Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Move.MoveDirect();
    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
