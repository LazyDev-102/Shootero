using Class_FSM;
using UnityEngine;

public class B05Attack1State : B05State {

    #region Singleton
    public B05Attack1State() {

    }
    private static B05Attack1State instance = null;
    public static B05Attack1State Instance {
        get {
            if (instance == null) {
                instance = new B05Attack1State();
            }
            return instance;
        }
    }
    #endregion
    private B05Transition[] transitions = { B05EndAttackTransition.Instance, B05CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B05Base> controller) {

    }

    protected override void DoStartActions(StateController<B05Base> controller) {
        controller.ObjectBase.B05Attack.ChooseAttack();
        controller.ObjectBase.B05Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B05Base> controller) {

    }

    protected override Transition<B05Base>[] GetTransitions() {
        return transitions;
    }
}
