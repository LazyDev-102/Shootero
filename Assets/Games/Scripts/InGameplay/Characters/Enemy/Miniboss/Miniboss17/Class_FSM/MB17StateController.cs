using Class_FSM;
using UnityEngine;

public class MB17StateController : StateController<MB17Base> {
    private MB17Transition[] transitions = { MB17IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB17Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB17StartState.Instance);
        MB17StartState.Instance.StartState(this);
    }
}
