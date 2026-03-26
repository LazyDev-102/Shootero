
using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillLaserScan", menuName = "Resource/HardData/Skill/SkillLaserScan")]
public class SkillLaserScan : ItemSkillData {
    [SerializeField] private BasicLaser bulletPrefab;
    [SerializeField] private float laserRadius = 1f;
    [SerializeField] private float angleRange;
    [SerializeField] private float distancePerLaser = 0.2f;
    [SerializeField] private float[] startAngle;
    [SerializeField] private int[] direction;
    [SerializeField] private ParticleSystem effect;

    private int numberBullet = 4;
    private float duration = 10f;
    private float deltaShot = 0.1f;
    private float percentDamage = 0.1f;
    private float aimRotate = 10f;
    private float timeOffPoint;
    private float[] amplitude;
    private int[] dir;
    private List<BasicLaser> bullets = new List<BasicLaser>();
    private Countdowner durationCountdowner = new Countdowner();
    private Countdowner deltaShotCountdowner = new Countdowner();

    public override void Preload() {
        if (effect != null)
            effect.RegisterPool(5);
        if (bulletPrefab != null)
            bulletPrefab.RegisterPool(5);
    }

    private void SetStatData() {
        duration = GetStat(SkillRankItemType.Duration);
        deltaShot = GetStat(SkillRankItemType.DeltaShot);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        numberBullet = (int)GetPrivateStat(SkillRankItemType.BulletCount);
        aimRotate = (int)GetPrivateStat(SkillRankItemType.BulletAimSpeed);
    }
    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ResetData();
        durationCountdowner.StartCountdown(duration);
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
            if (durationCountdowner.IsTimeOut()) {
                EndAttack(ship);
            }
            else {
                BeamingLaser();
            }
            durationCountdowner.Countdowning(Time.deltaTime);
        }
    }
    public override void ResetData() {
        for (int i = 0; i < bullets.Count; i++) {
            if (bullets[i] != null) {
                bullets[i].Recycle();
                GameManager.Instance.GameLoader.SpawnEffectExplosion(effect, bullets[i].transform.position - Vector3.up * 0.5f);
            }
        }
        bullets.Clear();
        dir = new int[direction.Length];
        amplitude = new float[direction.Length];
        for (int i = 0; i < dir.Length; i++) {
            dir[i] = direction[i];
        }
    }
    private void SpawnLaser() {
        int damage = (int)(ship.ShipStat.Atk.Value * percentDamage);
        timeOffPoint = duration * .2f;
        for (int i = 0; i < numberBullet; i++) {
            var pos = BorderHelper.GetRandomPointBottomBorder(distancePerLaser * (i + 1));
            GameManager.Instance.GameLoader.SpawnEffectExplosion(effect, pos - Vector2.up * 0.5f);
            var bClone = bulletPrefab.Spawn(pos);
            bClone.transform.localEulerAngles = new Vector3(0, 0, startAngle[i]);
            bClone.StartBeam();
            bClone.gameObject.SetActive(true);
            bClone.SetInfor(damage, null);
            bullets.Add(bClone);
        }
    }
    private void BeamingLaser() {
        deltaShotCountdowner.Countdowning(Time.deltaTime);
        float timeOffElapsed = durationCountdowner.Countdown;
        for (int i = 0; i < bullets.Count; i++) {
            if (timeOffElapsed < timeOffPoint) {
                float percentSize = timeOffElapsed / timeOffPoint;
                bullets[i].SetPercentSize(percentSize);
            }
            else
                bullets[i].SetPercentSize(1);
            Rotation(i);
        }
        if (deltaShotCountdowner.IsTimeOut()) {
            for (int i = 0; i < bullets.Count; i++) {
                bullets[i].Beaming(true);
            }
            deltaShotCountdowner.StartCountdown(deltaShot);
        }
        else {
            for (int i = 0; i < bullets.Count; i++) {
                bullets[i].Beaming(false);
            }
        }
    }
    private void Rotation(int index) {
        var temp = bullets[index].transform.localEulerAngles;
        amplitude[index] += Time.deltaTime * aimRotate;
        if (amplitude[index] > angleRange) {
            amplitude[index] = 0;
            dir[index] = dir[index] * -1;
        }
        temp.z += dir[index] * Time.deltaTime * aimRotate;
        bullets[index].transform.localEulerAngles = temp;
    }
    private void PlayEffect() {
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration, index),
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.DeltaShot, index),
                            GetStat(SkillRankItemType.CoolDown, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration),
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.DeltaShot),
                            GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.Duration)}<color=green>({GetNextStat(SkillRankItemType.Duration)})</color>",
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100}</color>)",
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({GetNextStat(SkillRankItemType.DeltaShot)})</color>",
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
    [System.Serializable]
    public class SkillRankData {
        [SerializeField] private float coolDown;
        [SerializeField] private float duration;

        public float CoolDown { get => coolDown; }
        public float Duration { get => duration; }
    }
}
