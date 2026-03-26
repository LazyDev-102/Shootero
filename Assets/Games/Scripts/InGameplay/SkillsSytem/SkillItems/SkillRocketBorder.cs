
using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRocketBorder", menuName = "Resource/HardData/Skill/SkillRocketBorder")]
public class SkillRocketBorder : ItemSkillData {
    [SerializeField] private BoomFrontBullet bullet;
    [SerializeField] private float boomRadius = 2;
    [SerializeField] private float acceleration = -1;
    [SerializeField] private Area spawnAre;
    [SerializeField] private Explosioner effect;

    private int numberBullet = 5;
    private float deltaShot = 0.5f;
    private float percentDamage = 5;
    private float bulletSpeed = 20;

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
        if (bullet != null) {
            bullet.RegisterPool(5);
            bullet.PreloadIngame();
        }
    }

    private void SetStatData() {
        deltaShot = GetStat(SkillRankItemType.DeltaShot);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        numberBullet = (int)GetStat(SkillRankItemType.BulletCount);
        bulletSpeed = GetPrivateStat(SkillRankItemType.BulletSpeed);
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ship.StartCoroutine(Shot());
        PlayEffect();
    }
    private IEnumerator Shot() {
        for (int i = 0; i < numberBullet; i++) {
            var pos = BorderHelper.GetWorldPointInsideArea(spawnAre);
            var bClone = GameManager.Instance.GameLoader.SpawnBullet(bullet, pos);
            bClone.SetHitInfor((int)(ship.ShipStat.Atk.Value * percentDamage), null, ship);
            bClone.SetMoveComplete(bClone.WarningEffect, 0)
                  .SetTarget((Vector3)pos + Vector3.up * 50)
                  .SetBoomRadius(boomRadius)
                  .Shoot(Vector2.up, bulletSpeed, acceleration);
            yield return Yielder.Wait(deltaShot);
        }
    }
    private void PlayEffect() {
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                        GetStat(SkillRankItemType.DeltaShot, index),
                        GetStat(SkillRankItemType.PercentDamage, index) * 100,
                        GetStat(SkillRankItemType.BulletCount, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                        GetStat(SkillRankItemType.DeltaShot),
                        GetStat(SkillRankItemType.PercentDamage) * 100,
                        GetStat(SkillRankItemType.BulletCount));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({GetNextStat(SkillRankItemType.DeltaShot)})</color>",
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.BulletCount)}<color=green>({GetNextStat(SkillRankItemType.BulletCount)})</color>");
    }

}
