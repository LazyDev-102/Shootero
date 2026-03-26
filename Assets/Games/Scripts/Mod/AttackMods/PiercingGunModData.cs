using UnityEngine;


[CreateAssetMenu(fileName = "PiercingGunModData", menuName = "Mod/Attack/PiercingGun")]

public class PiercingGunModData : AttackModData {
    //[SerializeField] private PlayerPiercingGunComponent piercingComponent;

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        //character.AttackerPlayer.ChangeAttackComponent(piercingComponent);
    }
}
