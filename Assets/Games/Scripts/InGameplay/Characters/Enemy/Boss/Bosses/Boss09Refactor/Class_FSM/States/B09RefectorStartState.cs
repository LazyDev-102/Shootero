

using Class_FSM;
using UnityEngine;

public class B09RefectorStartState : B09RefectorState {
    #region Singleton
    public B09RefectorStartState() {

    }
    private static B09RefectorStartState instance = null;
    public static B09RefectorStartState Instance {
        get {
            if(instance == null) {
                instance = new B09RefectorStartState();
            }
            return instance;
        }
    }
    #endregion
    private B09RefectorTransition[] transitions = { B09RefectorCanAppearTransition.Instance };
    public override Color SceneGizmoColor => Color.white;
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {
    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.Spawn();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {
    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }
}
