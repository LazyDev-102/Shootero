

using Class_FSM;
using UnityEngine;

public class B01AttackState : B01State {

    #region Singleton
    public B01AttackState() {

    }
    private static B01AttackState instance = null;
    public static B01AttackState Instance {
        get {
            if (instance == null) {
                instance = new B01AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B01Transition[] transitions = { B01EndAttackTransition.Instance, B01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B01Base> controller) {

    }

    protected override void DoStartActions(StateController<B01Base> controller) {
        controller.ObjectBase.B01Attack.ChooseAttack();
        controller.ObjectBase.B01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B01Base> controller) {

    }

    protected override Transition<B01Base>[] GetTransitions() {
        return transitions;
    }
}
