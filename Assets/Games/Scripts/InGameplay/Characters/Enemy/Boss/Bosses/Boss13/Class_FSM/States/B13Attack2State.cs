using Class_FSM;
using UnityEngine;

public class B13Attack2State : B13State {

    #region Singleton
    public B13Attack2State() {

    }
    private static B13Attack2State instance = null;
    public static B13Attack2State Instance {
        get {
            if (instance == null) {
                instance = new B13Attack2State();
            }
            return instance;
        }
    }
    #endregion
    private B13Transition[] transitions = { B13EndAttackTransition.Instance, B13CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B13Base> controller) {

    }

    protected override void DoStartActions(StateController<B13Base> controller) {
        controller.ObjectBase.B13Attack.ChooseAttack();
        controller.ObjectBase.B13Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B13Base> controller) {

    }

    protected override Transition<B13Base>[] GetTransitions() {
        return transitions;
    }
}
