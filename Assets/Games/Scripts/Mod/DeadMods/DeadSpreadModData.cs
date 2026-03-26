using Gemmob;
using Helper;
using UnityEngine;

[CreateAssetMenu(fileName = "DeadSpreadModData", menuName = "Mod/KillModData/DeadSpread")]
public class DeadSpreadModData : KillModData {
    [SerializeField] private int chance;
    [SerializeField] private FrontBullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletAcceler;
    [SerializeField] private int numberDirection;
    [SerializeField] private float damagePercent;
    [SerializeField] private int numberPreload;

    public int Chance { get => chance; }
    public FrontBullet Bullet { get => bullet; }
    public float BulletSpeed { get => bulletSpeed; }
    public float BulletAcceler { get => bulletAcceler; }
    public int NumberDirection { get => numberDirection; }
    public float DamagePercent { get => damagePercent; }

    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        DeadSpreadModInfo modInfo = new DeadSpreadModInfo(this);
        character.ShipSkill.AddKillAttackMod(modInfo);
    }

    public override void PreloadOpenApp() {
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }
}


public class DeadSpreadModInfo : KillModInfor<DeadSpreadModData> {
    private bool isSuperBullet;

    public DeadSpreadModInfo(DeadSpreadModData modData) : base(modData) {
        isSuperBullet = false;
    }

    public DeadSpreadModInfo(DeadSpreadModInfo mod) : base(mod) {

    }

    public void UpgradeSuperBullet() {
        isSuperBullet = true;
    }

    public override void ActionKill(ShipBase killer, CharacterBase victim) {
        if (RandomHelper.RandomWithProbability(modData.Chance)) {
            Vector2 position = victim.transform.position;
            killer.DelayFrame(1, () => {
                Spreading(killer, position);
            });
        }
    }


    private void Spreading(ShipBase ship, Vector2 position) {
        Vector2 directionBase = UnityHelper.Down;
        float deltaAngle = 360f / modData.NumberDirection;
        for (int ibullet = 0; ibullet < modData.NumberDirection; ++ibullet) {
            Vector2 direction = directionBase.RotateDirection(ibullet * deltaAngle);
            FrontBullet newBullet = GameManager.Instance.GameLoader.SpawnBullet(modData.Bullet, position);
            if (newBullet) {
                newBullet = ChangingBullet(newBullet, ship);
                newBullet.Shoot(modData.BulletSpeed, direction, modData.BulletAcceler);
            }
        }
    }

    private FrontBullet ChangingBullet(FrontBullet bullet, ShipBase ship) {
        int damage = Mathf.CeilToInt(ship.ShipStat.GetFinalDamageWeapon * modData.DamagePercent);
        if (isSuperBullet) {
            bullet.SetHitInfor(damage, ship.ShipSkill.EffectAttackMods, ship, ship.ShipStat.CritChance.Value, ship.ShipStat.CritDamage.Value);
            foreach (var mod in ship.ShipSkill.ChangeBulletMods) {
                mod.ChangeBullet(bullet);
            }
        }
        else {
            bullet.SetHitInfor(damage, null, ship);
        }
        return bullet;
    }

    public override object Clone() {
        return new DeadSpreadModInfo(this);
    }
}


