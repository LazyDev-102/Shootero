

using Class_FSM;
using UnityEngine;

public class B05AppearState : B05State {
    #region Singleton
    public B05AppearState() {

    }
    private static B05AppearState instance = null;
    public static B05AppearState Instance {
        get {
            if(instance == null) {
                instance = new B05AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B05Transition[] transitions = { B05AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B05Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Move.MoveDirect();
    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
