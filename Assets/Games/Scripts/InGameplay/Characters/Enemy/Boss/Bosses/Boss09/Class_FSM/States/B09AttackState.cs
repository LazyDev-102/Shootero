

using Class_FSM;
using UnityEngine;

public class B09AttackState : B09State {

    #region Singleton
    public B09AttackState() {

    }
    private static B09AttackState instance = null;
    public static B09AttackState Instance {
        get {
            if (instance == null) {
                instance = new B09AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B09Transition[] transitions = { B09EndAttackTransition.Instance, B09CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B09Base> controller) {

    }

    protected override void DoStartActions(StateController<B09Base> controller) {
        controller.ObjectBase.B09Attack.ChooseAttack();
        controller.ObjectBase.B09Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B09Base> controller) {

    }

    protected override Transition<B09Base>[] GetTransitions() {
        return transitions;
    }
}
