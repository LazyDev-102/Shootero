

using Class_FSM;
using UnityEngine;

public class B11AppearState : B11State {
    #region Singleton
    public B11AppearState() {

    }
    private static B11AppearState instance = null;
    public static B11AppearState Instance {
        get {
            if(instance == null) {
                instance = new B11AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B11Transition[] transitions = { B11AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B11Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Move.MoveDirect();
    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
