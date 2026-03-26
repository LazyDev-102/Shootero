using UnityEngine;


[CreateAssetMenu(fileName = "PatternModData", menuName = "Mod/Pattern/PatternModData", order = 1)]
public class PatternModData : ModData {
    [SerializeField] private ShotPatternType patternType;
    protected ShipPattern shipPattern;


    public void SetShipPattern(ShipPattern shipPattern) {
        this.shipPattern = shipPattern;
    }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        if (shipPattern) {
            character.ShipAttack.ChangePattern(shipPattern);
            character.ShipAttack.PatternType = patternType;
            GameResources.Instance.ModGenerator.SetCurrentPatternMod(this);
        }
    }
}


