
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillOneLaserTarget", menuName = "Resource/HardData/Skill/SkillOneLaserTarget")]
public class SkillOneLaserTarget : ItemSkillData {
    [SerializeField] private OneLaserTarget laserPrefab;
    [SerializeField] private Explosioner effect;

    private float fireRate = 5f;
    private float duration = 10f;
    private float deltaShot = 0.1f;
    private float percentDamage = 1;

    private OneLaserTarget laser;

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
        if (laserPrefab != null) {
            laserPrefab.RegisterPool(1);
        }
    }

    private void SetStatData() {
        deltaShot = GetStat(SkillRankItemType.DeltaShot);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        fireRate = GetStat(SkillRankItemType.FireRate);
        duration = GetStat(SkillRankItemType.Duration);
    }
    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        SpawnLaser();
        PlayEffect();
        canAttack = true;
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        canAttack = false;
        laser.Recycle();
    }
    public override void Updating() {
        if (canAttack) {
            laser.Updating();
        }
    }
    private void SpawnLaser() {
        laser = laserPrefab.Spawn(ship.transform);
        laser.transform.localPosition = Vector3.zero;
        laser.gameObject.SetActive(true);
        laser.Init(ship, fireRate, duration, deltaShot, percentDamage);
    }
    private void PlayEffect() {
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration, index),
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.DeltaShot, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration),
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.DeltaShot));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.Duration)}<color=green>({GetNextStat(SkillRankItemType.Duration)})</color>",
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({ GetNextStat(SkillRankItemType.DeltaShot)})</color>");
    }
}
