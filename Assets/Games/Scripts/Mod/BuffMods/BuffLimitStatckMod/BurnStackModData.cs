using UnityEngine;


[CreateAssetMenu(fileName = "BurnStackModData", menuName = "Mod/Buff/Limited/BurnStack")]
public class BurnStackModData : BuffLimitStackModData {
    [SerializeField] private BurnShotModData burnMod;
    [SerializeField] private int addBurnStack;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        BurnShotModInfor burnInfor = character.ShipSkill.GetModInfor<BurnShotModInfor>(burnMod.ModId);
        burnInfor.MaxBurnStack += addBurnStack;
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
}
