using UnityEngine;

[CreateAssetMenu(fileName = "AbilityFreeReroll", menuName = "Resource/HardData/Ability/AbilityFreeReroll")]
public class AbilityFreeReroll : NewAbilityItemData {
    [SerializeField] private bool active;

    public bool Active { get => active; }

    protected override void OnEnable() {
        base.OnEnable();
        active = false;
    }
    protected override void Install() {
        active = true;
    }
    protected override void Unistall() {
        active = false;
    }
    public override void Apply(ShipBase ship) {
        if (Unlocked) {
            active = true;
        }
    }
}
