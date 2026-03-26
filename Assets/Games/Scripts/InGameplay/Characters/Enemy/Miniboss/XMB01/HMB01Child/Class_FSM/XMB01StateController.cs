using Class_FSM;
using UnityEngine;

public class XMB01StateController : StateController<XMB01Base> {
    private XMB01Transition[] transitions = { XMB01IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<XMB01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(XMB01StartState.Instance);
        XMB01StartState.Instance.StartState(this);
    }
}
