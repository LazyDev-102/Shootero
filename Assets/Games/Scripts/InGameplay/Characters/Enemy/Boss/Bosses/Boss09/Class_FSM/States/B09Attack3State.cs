using Class_FSM;
using UnityEngine;

public class B09Attack3State : B09State {

    #region Singleton
    public B09Attack3State() {

    }
    private static B09Attack3State instance = null;
    public static B09Attack3State Instance {
        get {
            if (instance == null) {
                instance = new B09Attack3State();
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
