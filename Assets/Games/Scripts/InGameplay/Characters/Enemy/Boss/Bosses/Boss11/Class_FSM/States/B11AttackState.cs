

using Class_FSM;
using UnityEngine;

public class B11AttackState : B11State {

    #region Singleton
    public B11AttackState() {

    }
    private static B11AttackState instance = null;
    public static B11AttackState Instance {
        get {
            if (instance == null) {
                instance = new B11AttackState();
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
