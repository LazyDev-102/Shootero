using UnityEngine;


[CreateAssetMenu(fileName = "BulletUpModData", menuName = "Mod/Buff/Limited/BulletUp")]
public class BulletUpModData : BuffLimitStackModData {
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        character.ShipAttack.BulletUp();
        character.ShipSkill.AddModInfo(new BuffLimitStackModInfo(this));
    }
    public override bool FirstCondition(ShipBase character) {
        if (GameResources.Instance.ConquerorData.IsTut)
            return true;

        var p = IngameHUD.Instance.Combat;
        var currentLevel = 0;
        if (p) {
            currentLevel = p.GetCurrentLevelInGame();
        }

        if (GameResources.Instance.ConquerorData.IsTutPlayGame) {
            return currentLevel < 4 || currentLevel >= 8;
        }
        return true;
        //var count = character.ShipSkill.GetCountMod(this);
        //switch (count) {
        //    case 0:
        //        return Helper.RandomHelper.RandomWithPercent(50);
        //    case 1:
        //        return Helper.RandomHelper.RandomWithPercent(20);
        //    case 2:
        //        return Helper.RandomHelper.RandomWithPercent(10);
        //    default:
        //        return false;
        //}
    }
}
