

using Class_FSM;

public class ChestChip01StateController : StateController<ChestChip01Base> {

    private ChestChip01Transition[] transitions = { ChestChip01IsDieTransition.Instance };
    protected override void DoAlwaysActions() {
    }

    protected override Transition<ChestChip01Base>[] GetTransitionFromAnyState() {
        return transitions;
    }

    protected override void StartStatrState() {
        SetCurrentState(ChestChip01StartState.Instance);
        ChestChip01StartState.Instance.StartState(this);
    }
}
