
using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillPassiveTwoLaser", menuName = "Resource/HardData/Skill/SkillPassiveTwoLaser")]
public class SkillPassiveTwoLaser : ItemSkillData {
    [SerializeField] private PierceLaser bulletPrefab;
    [SerializeField] private Vector3 offsetPosL;
    [SerializeField] private Vector3 offsetPosR;
    [SerializeField] private int numberBullet = 2;
    [SerializeField] private float laserRadius = 1f;
    [SerializeField] private float angleRange;
    [SerializeField] private float[] startAngle;
    [SerializeField] private int[] direction;
    [SerializeField] private ParticleSystem effect;

    private float deltaShot = 0.1f;
    private float speedRotate = 10f;
    private float percentDamage = 0.1f;
    private int damage = 0;
    private float[] amplitude;
    private int[] dir;
    private List<PierceLaser> bullets = new List<PierceLaser>();
    private Countdowner deltaShotCd = new Countdowner();

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
        if (bulletPrefab != null) {
            bulletPrefab.RegisterPool(2);
        }
    }

    private void SetStatData() {
        deltaShot = GetStat(SkillRankItemType.DeltaShot);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        speedRotate = GetPrivateStat(SkillRankItemType.BulletSpeed);
    }
    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ResetData();
        SpawnLaser();
        PlayEffect();
        canAttack = true;
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        ResetData();
        canAttack = false;
    }
    public override void Updating() {
        if (canAttack) {
            BeamingLaser();
        }
    }
    private void SpawnLaser() {
        damage = (int)(ship.ShipStat.Atk.Value * percentDamage);
        for (int i = 0; i < numberBullet; i++) {
            var pos = i == 0 ? ship.transform.position + offsetPosL : ship.transform.position + offsetPosR;
            var bClone = bulletPrefab.Spawn(ship.transform, pos);
            bClone.transform.localEulerAngles = new Vector3(0, 0, startAngle[i]);
            bClone.StartBeam();
            bClone.gameObject.SetActive(true);
            bClone.SetInfor(damage, null);
            bullets.Add(bClone);
        }
    }
    public void BeamingLaser() {
        deltaShotCd.Countdowning(Time.deltaTime);
        Rotation(0);
        Rotation(1);
        if (deltaShotCd.IsTimeOut()) {
            bullets[0].Beaming(true);
            bullets[1].Beaming(true);
            deltaShotCd.StartCountdown(deltaShot);
        }
        else {
            bullets[0].Beaming(false);
            bullets[1].Beaming(false);
        }
    }
    private void Rotation(int index) {
        var temp = bullets[index].transform.localEulerAngles;
        amplitude[index] += Time.deltaTime * speedRotate;
        if (amplitude[index] > angleRange) {
            amplitude[index] = 0;
            dir[index] = dir[index] * -1;
        }
        temp.z += dir[index] * Time.deltaTime * 10f;
        bullets[index].transform.localEulerAngles = temp;
    }
    public override void ResetData() {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != null)
                bullets[i].Recycle();
        }
        bullets.Clear();
        dir = new int[direction.Length];
        amplitude = new float[direction.Length];
        for (int i = 0; i < dir.Length; i++) {
            dir[i] = direction[i];
        }
    }
    private void PlayEffect() {
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.DeltaShot, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.DeltaShot));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({GetNextStat(SkillRankItemType.DeltaShot)})</color>");
    }
}
