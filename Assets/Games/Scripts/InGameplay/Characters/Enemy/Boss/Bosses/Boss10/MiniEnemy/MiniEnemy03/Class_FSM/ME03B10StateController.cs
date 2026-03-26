using Class_FSM;
using UnityEngine;

public class ME03B10StateController : StateController<ME03B10Base> {

    private ME03B10Transition[] transitons = { ME03B10IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<ME03B10Base>[] GetTransitionFromAnyState() {
        return transitons;
    }

    protected override void StartStatrState() {
        SetCurrentState(ME03B10StartState.Instance);
        ME03B10StartState.Instance.StartState(this);
    }
}
