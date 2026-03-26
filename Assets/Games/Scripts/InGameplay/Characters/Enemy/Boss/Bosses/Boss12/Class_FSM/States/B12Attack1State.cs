using Class_FSM;
using UnityEngine;

public class B12Attack1State : B12State {

    #region Singleton
    public B12Attack1State() {

    }
    private static B12Attack1State instance = null;
    public static B12Attack1State Instance {
        get {
            if (instance == null) {
                instance = new B12Attack1State();
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
