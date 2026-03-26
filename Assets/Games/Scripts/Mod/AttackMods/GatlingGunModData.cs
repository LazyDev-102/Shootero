using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "GatlingGunModData", menuName = "Mod/Attack/GatlingGun")]

public class GatlingGunModData : AttackModData {
    //[SerializeField] private PlayerGatlingGunComponent playerGatlingGunComponent;

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        //character.AttackerPlayer.ChangeAttackComponent(playerGatlingGunComponent);
    }
}
