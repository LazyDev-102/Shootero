using UnityEngine;

[CreateAssetMenu(fileName = "AbilityLevelUpHeal", menuName = "Resource/HardData/Ability/AbilityLevelUpHeal")]
public class AbilityLevelUpHeal : NewAbilityItemData {
    [SerializeField] private float healPercent;

    protected override void OnEnable() {
        base.OnEnable();
        Gemmob.EventDispatcher.Instance.AddListener<EventKey.OnShipLevelUpInGame>(OnShipLevelUpIngame);
    }

    private void OnDisable() {
        Gemmob.EventDispatcher.Instance.RemoveListener<EventKey.OnShipLevelUpInGame>(OnShipLevelUpIngame);
    }

    private void OnShipLevelUpIngame(EventKey.OnShipLevelUpInGame param) {
        ApplyIngame(param.Ship);
    }

    public override void ApplyIngame(ShipBase ship) {
        if (Unlocked && ship != null) {
            base.ApplyIngame(ship);
            int maxHp = ship.ShipStat.MaxHP.Value;
            int healHp = Mathf.CeilToInt(maxHp * healPercent / 100);
            ship.ShipHealth.AddHpWithHealingEffect(healHp, true);
        }
    }
}
