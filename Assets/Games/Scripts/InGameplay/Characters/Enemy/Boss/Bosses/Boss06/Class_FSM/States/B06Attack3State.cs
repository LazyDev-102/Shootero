using Class_FSM;
using UnityEngine;

public class B06Attack3State : B06State {

    #region Singleton
    public B06Attack3State() {

    }
    private static B06Attack3State instance = null;
    public static B06Attack3State Instance {
        get {
            if (instance == null) {
                instance = new B06Attack3State();
            }
            return instance;
        }
    }
    #endregion
    private B06Transition[] transitions = { B06EndAttackTransition.Instance, B06CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<B06Base> controller) {

    }

    protected override void DoStartActions(StateController<B06Base> controller) {
        controller.ObjectBase.B06Attack.ChooseAttack();
        controller.ObjectBase.B06Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<B06Base> controller) {

    }

    protected override Transition<B06Base>[] GetTransitions() {
        return transitions;
    }
}
