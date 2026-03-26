

using Class_FSM;
using UnityEngine;

public class B09RefectorAppearState : B09RefectorState {
    #region Singleton
    public B09RefectorAppearState() {

    }
    private static B09RefectorAppearState instance = null;
    public static B09RefectorAppearState Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorAppearState();
            }
            return instance;
        }
    }
    #endregion

    private B09RefectorTransition[] transitions = { B09RefectorAppearCompleteTransition.Instance };
    public override Color SceneGizmoColor => Color.yellow;
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.StartIdleAfterAppear();
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.B09RefectorMove.StartMoveAppear();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.B09RefectorMove.MoveDirect();
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }
}
