using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "HealItem", menuName = "Resource/Item/Obstacles/HealItem")]
public class HealItem : Item {
    [SerializeField, Range(0f, 1f)] private float percentHeal;
    public override void Claim(int amount) {
        var ship = GameManager.Instance.GameLoader.Ship;
        if (ship)
            ship.ShipHealth.AddHpWithHealingEffect((int)(ship.ShipStat.MaxHP.Value * percentHeal * amount), hasBloodSucking: true);
    }
}