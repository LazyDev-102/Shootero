
using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillPassiveRocketTarget", menuName = "Resource/HardData/Skill/SkillPassiveRocketTarget")]
public class SkillPassiveRocketTarget : ItemSkillData {
    [SerializeField] private RocketBullet bulletPrefab;
    [SerializeField] private float offset = 1;

    private int numberBullet = 2;
    private float fireRate = 2f;
    private float percentDamage = 1;
    private float speed;
    private float delayAttack = 2;
    private int damage = 0;
    private float timeHoming;
    private Countdowner fireRateCd = new Countdowner();
    private List<RocketBullet> bullets = new List<RocketBullet>();

    public override void Preload() {
        if (bulletPrefab != null) {
            bulletPrefab.RegisterPool(numberBullet * 2);
            bulletPrefab.PreloadIngame();
        }
    }

    private void SetStatData() {
        fireRate = GetStat(SkillRankItemType.FireRate);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        numberBullet = (int)GetPrivateStat(SkillRankItemType.BulletCount);
        speed = GetPrivateStat(SkillRankItemType.BulletSpeed);
        delayAttack = GetPrivateStat(SkillRankItemType.DelayHoming);
        timeHoming = GetPrivateStat(SkillRankItemType.TimeHoming);
    }
    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ResetData();
        canAttack = true;
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        ResetData();
        canAttack = false;
    }
    public override void Updating() {
        if (canAttack) {
            if (fireRateCd.IsTimeOut()) {
                SpawnBullet();
                fireRateCd.StartCountdown(fireRate);
            }
            else {
                fireRateCd.Countdowning(Time.deltaTime);
            }
        }
    }
    private void SpawnBullet() {
        damage = (int)(ship.ShipStat.Atk.Value * percentDamage);
        for (int i = 0; i < numberBullet; i++) {
            var bClone = GameManager.Instance.GameLoader.SpawnBullet(bulletPrefab, ship.transform.position);
            bClone.SetHitInfor(damage, null, null);
            bClone.Shoot(i % 2 == 0, speed, offset, delayAttack);
            bullets.Add(bClone);
        }
    }
    public override void ResetData() {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != null)
                bullets[i].Recycle();
        }
        bullets.Clear();
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.FireRate, index),
                            GetStat(SkillRankItemType.PercentDamage, index) * 100);
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.FireRate),
                            GetStat(SkillRankItemType.PercentDamage) * 100);
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.FireRate)}<color=green>({ GetNextStat(SkillRankItemType.FireRate)})</color>",
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({ GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>");
    }
}
