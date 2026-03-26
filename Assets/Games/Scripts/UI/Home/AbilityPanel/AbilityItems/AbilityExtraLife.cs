using UnityEngine;

[CreateAssetMenu(fileName = "AbilityExtraLife", menuName = "Resource/HardData/Ability/AbilityExtraLife")]
public class AbilityExtraLife : NewAbilityItemData {

    protected override void OnEnable() {
        base.OnEnable();
        Gemmob.EventDispatcher.Instance.AddListener<EventKey.OnStartGame>(Action);
    }

    private void OnDisable() {
        Gemmob.EventDispatcher.Instance.RemoveListener<EventKey.OnStartGame>(Action);
    }

    private void Action() {
        ApplyIngame(GameManager.Instance.GameLoader.Ship);
    }

    public override void ApplyIngame(ShipBase ship) {
        if (Unlocked && ship != null) {
            base.ApplyIngame(ship);
            ship.AddLives();
        }
    }
}
