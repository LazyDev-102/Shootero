

using Class_FSM;
using UnityEngine;

public class B14AttackState : B14State {

    #region Singleton
    public B14AttackState() {

    }
    private static B14AttackState instance = null;
    public static B14AttackState Instance {
        get {
            if (instance == null) {
                instance = new B14AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B14Transition[] transitions = { B14EndAttackTransition.Instance, B14CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B14Base> controller) {

    }

    protected override void DoStartActions(StateController<B14Base> controller) {
        controller.ObjectBase.B14Attack.ChooseAttack();
        controller.ObjectBase.B14Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B14Base> controller) {

    }

    protected override Transition<B14Base>[] GetTransitions() {
        return transitions;
    }
}
