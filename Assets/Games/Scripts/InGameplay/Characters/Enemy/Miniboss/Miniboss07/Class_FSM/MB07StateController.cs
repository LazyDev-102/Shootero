using Class_FSM;
using UnityEngine;

public class MB07StateController : StateController<MB07Base> {
    private MB07Transition[] transitions = { MB07IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB07Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB07StartState.Instance);
        MB07StartState.Instance.StartState(this);
    }
}
