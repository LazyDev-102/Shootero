using Class_FSM;
using UnityEngine;

public class B11Attack3State : B11State {

    #region Singleton
    public B11Attack3State() {

    }
    private static B11Attack3State instance = null;
    public static B11Attack3State Instance {
        get {
            if (instance == null) {
                instance = new B11Attack3State();
            }
            return instance;
        }
    }
    #endregion
    private B11Transition[] transitions = { B11EndAttackTransition.Instance, B11CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B11Base> controller) {

    }

    protected override void DoStartActions(StateController<B11Base> controller) {
        controller.ObjectBase.B11Attack.ChooseAttack();
        controller.ObjectBase.B11Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B11Base> controller) {

    }

    protected override Transition<B11Base>[] GetTransitions() {
        return transitions;
    }
}
