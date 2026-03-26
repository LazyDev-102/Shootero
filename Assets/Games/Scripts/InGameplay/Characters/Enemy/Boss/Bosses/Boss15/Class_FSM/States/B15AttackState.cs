

using Class_FSM;
using UnityEngine;

public class B15AttackState : B15State {

    #region Singleton
    public B15AttackState() {

    }
    private static B15AttackState instance = null;
    public static B15AttackState Instance {
        get {
            if (instance == null) {
                instance = new B15AttackState();
            }
            return instance;
        }
    }
    #endregion
    private B15Transition[] transitions = { B15EndAttackTransition.Instance, B15CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B15Base> controller) {

    }

    protected override void DoStartActions(StateController<B15Base> controller) {
        controller.ObjectBase.B15Attack.ChooseAttack();
        controller.ObjectBase.B15Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B15Base> controller) {

    }

    protected override Transition<B15Base>[] GetTransitions() {
        return transitions;
    }
}
