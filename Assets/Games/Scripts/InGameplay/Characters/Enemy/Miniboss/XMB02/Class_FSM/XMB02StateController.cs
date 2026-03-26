using Class_FSM;
using UnityEngine;

public class XMB02StateController : StateController<XMB02Base> {
    private XMB02Transition[] transitions = { XMB02IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<XMB02Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(XMB02StartState.Instance);
        XMB02StartState.Instance.StartState(this);
    }
}
