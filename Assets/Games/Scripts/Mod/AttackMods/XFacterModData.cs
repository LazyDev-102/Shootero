using UnityEngine;

[CreateAssetMenu(fileName = "XFacterModData", menuName = "Mod/Attack/XFacter")]

public class XFacterModData : AttackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
    }
}