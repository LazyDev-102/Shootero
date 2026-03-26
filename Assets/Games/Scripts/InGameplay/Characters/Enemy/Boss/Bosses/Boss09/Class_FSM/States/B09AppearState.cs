

using Class_FSM;
using UnityEngine;

public class B09AppearState : B09State {
    #region Singleton
    public B09AppearState() {

    }
    private static B09AppearState instance = null;
    public static B09AppearState Instance {
        get {
            if(instance == null) {
                instance = new B09AppearState();
            }
            return instance;
        }
    }
    #endregion

    private B09Transition[] transitions = { B09AppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B09Base> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Move.MoveDirect();
    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}
