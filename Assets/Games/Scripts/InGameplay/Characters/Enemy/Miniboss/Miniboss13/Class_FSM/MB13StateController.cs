using Class_FSM;
using UnityEngine;

public class MB13StateController : StateController<MB13Base> {
    private MB13Transition[] transitions = { MB13IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB13Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB13StartState.Instance);
        MB13StartState.Instance.StartState(this);
    }
}
