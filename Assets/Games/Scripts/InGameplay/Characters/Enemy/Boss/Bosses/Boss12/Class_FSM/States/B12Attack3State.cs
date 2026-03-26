using Class_FSM;
using UnityEngine;

public class B12Attack3State : B12State {

    #region Singleton
    public B12Attack3State() {

    }
    private static B12Attack3State instance = null;
    public static B12Attack3State Instance {
        get {
            if (instance == null) {
                instance = new B12Attack3State();
            }
            return instance;
        }
    }
    #endregion
    private B12Transition[] transitions = { B12EndAttackTransition.Instance, B12CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B12Base> controller) {

    }

    protected override void DoStartActions(StateController<B12Base> controller) {
        controller.ObjectBase.B12Attack.ChooseAttack();
        controller.ObjectBase.B12Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B12Base> controller) {

    }

    protected override Transition<B12Base>[] GetTransitions() {
        return transitions;
    }
}
