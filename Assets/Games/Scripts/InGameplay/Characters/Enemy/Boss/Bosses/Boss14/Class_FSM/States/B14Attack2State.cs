using Class_FSM;
using UnityEngine;

public class B14Attack2State : B14State {

    #region Singleton
    public B14Attack2State() {

    }
    private static B14Attack2State instance = null;
    public static B14Attack2State Instance {
        get {
            if (instance == null) {
                instance = new B14Attack2State();
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
