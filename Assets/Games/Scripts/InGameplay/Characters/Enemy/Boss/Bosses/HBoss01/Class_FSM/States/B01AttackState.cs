

using Class_FSM;
using UnityEngine;

public class HB01AttackState : HB01State {

    #region Singleton
    public HB01AttackState() {

    }
    private static HB01AttackState instance = null;
    public static HB01AttackState Instance {
        get {
            if (instance == null) {
                instance = new HB01AttackState();
            }
            return instance;
        }
    }
    #endregion
    private HB01Transition[] transitions = { HB01EndAttackTransition.Instance, HB01CanRageTransition.Instance };
    public override Color SceneGizmoColor => Color.red;
    protected override void DoEndActions(StateController<HB01Base> controller) {

    }

    protected override void DoStartActions(StateController<HB01Base> controller) {
        controller.ObjectBase.HB01Attack.ChooseAttack();
        controller.ObjectBase.HB01Attack.Attack();
    }

    protected override void DoUpdateActions(StateController<HB01Base> controller) {

    }

    protected override Transition<HB01Base>[] GetTransitions() {
        return transitions;
    }
}
