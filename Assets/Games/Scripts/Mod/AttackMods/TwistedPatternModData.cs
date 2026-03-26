using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TwistedPatternModData", menuName = "Mod/Pattern/TwistedPatternModData")]
public class TwistedPatternModData : PatternModData {
    [SerializeField] private ShipAttackComponent[] shipAttackTwistedComponents;
    public override void ApplyTo(ShipBase character) {
        character.ShipSkill.AddModData(this);
        if (shipPattern) {
            character.ShipAttack.AddAttackComponent(GetShipAttackComponent());
            character.ShipAttack.ChangePattern(shipPattern);
            GameResources.Instance.ModGenerator.SetCurrentPatternMod(this);
        }
    }
    private ShipAttackComponent GetShipAttackComponent() {
        var shipIndex = GameResources.Instance.Ship.CurrentShip - 1;
        if (shipAttackTwistedComponents.Length < GameResources.Instance.Ship.CurrentShip)
            return null;
        return shipAttackTwistedComponents[shipIndex];
    }
}
