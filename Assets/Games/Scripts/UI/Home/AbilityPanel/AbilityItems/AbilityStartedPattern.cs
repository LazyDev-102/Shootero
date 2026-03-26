using UnityEngine;

[CreateAssetMenu(fileName = "AbilityStartedPattern", menuName = "Resource/HardData/Ability/AbilityStartedPattern")]
public class AbilityStartedPattern : NewAbilityItemData {
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
            base.Apply(ship);
            active = true;
        }
    }
}
