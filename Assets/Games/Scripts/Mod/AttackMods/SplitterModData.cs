using UnityEngine;


[CreateAssetMenu(fileName = "SplitterModData", menuName = "Mod/Attack/Splitter")]

public class SplitterModData : AttackModData {
    // [SerializeField] private PlayerSplitterComponent playerSplitterComponent;

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        //character.AttackerPlayer.ChangeAttackComponent(playerSplitterComponent);
    }
}
