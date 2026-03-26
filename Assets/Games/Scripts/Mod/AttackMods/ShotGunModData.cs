using UnityEngine;

[CreateAssetMenu(fileName = "ShotGunModData", menuName = "Mod/Attack/ShotGun")]

public class ShotGunModData : AttackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
    }
}