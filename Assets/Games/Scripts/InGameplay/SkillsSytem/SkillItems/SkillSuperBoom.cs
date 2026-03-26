
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSuperBoom", menuName = "Resource/HardData/Skill/SkillSuperBoom")]
public class SkillSuperBoom : ItemSkillData {
    [SerializeField] private BoomFrontBullet bullet;
    [SerializeField] private float radius = 3;
    [SerializeField] private float speed = 10;
    [SerializeField] private float acceleration = -1;
    [SerializeField] private Explosioner effect;

    private float percentDamage = 5;

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
        if (bullet != null) {
            bullet.RegisterPool(1);
            bullet.PreloadIngame();
        }
    }

    private void SetStatData() {
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        var bClone = GameManager.Instance.GameLoader.SpawnBullet(bullet, ship.transform.position);
        bClone.SetHitInfor((int)(ship.ShipStat.Atk.Value * percentDamage), null, ship);
        bClone.SetMoveComplete(bClone.WarningEffect, 0)
              .SetTarget(ship.transform.up + Vector3.up * 20)
              .SetBoomRadius(radius)
              .Shoot(Vector2.up, speed, acceleration);
        PlayEffect();
    }
    private void PlayEffect() {
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.CoolDown, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
}
