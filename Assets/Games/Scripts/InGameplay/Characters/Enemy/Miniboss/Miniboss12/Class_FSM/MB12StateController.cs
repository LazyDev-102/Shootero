using Class_FSM;
using UnityEngine;

public class MB12StateController : StateController<MB12Base> {
    private MB12Transition[] transitions = { MB12IsDeadTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<MB12Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(MB12StartState.Instance);
        MB12StartState.Instance.StartState(this);
    }
}
