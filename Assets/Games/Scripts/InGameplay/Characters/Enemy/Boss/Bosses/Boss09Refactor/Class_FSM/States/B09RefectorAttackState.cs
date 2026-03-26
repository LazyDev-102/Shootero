

using Class_FSM;
using UnityEngine;

public class B09RefectorAttackState : B09RefectorState {

    #region Singleton
    public B09RefectorAttackState() {

    }
    private static B09RefectorAttackState instance = null;
    public static B09RefectorAttackState Instance {
        get {
            if (instance == null) {
                instance = new B09RefectorAttackState();
            }
            return instance;
        }
    }
    #endregion
    private B09RefectorTransition[] transitions = { B09RefectorEndAttackTransition.Instance, B09RefectorCanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B09RefectorBase> controller) {

    }

    protected override void DoStartActions(StateController<B09RefectorBase> controller) {
        controller.ObjectBase.B09RefectorAttack.ChooseAttack();
        controller.ObjectBase.B09RefectorAttack.Attack();
    }

    protected override void DoUpdateActions(StateController<B09RefectorBase> controller) {

    }

    protected override Transition<B09RefectorBase>[] GetTransitions() {
        return transitions;
    }
}
