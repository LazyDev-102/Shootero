
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillLightsaber", menuName = "Resource/HardData/Skill/SkillLightsaber")]
public class SkillLightsaber : ItemSkillData {
    [SerializeField] private Lightsaber lightsaberPrefab;
    [SerializeField] private Explosioner effect;
    private float percentDamage = 0.5f;
    private float rotationSpeed = 10;

    private Lightsaber lightsaber;

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
        if (lightsaberPrefab != null) {
            lightsaberPrefab.RegisterPool(1);
        }
    }

    private void SetStatData() {
        //deltaShot = GetStat(SkillRankItemType.DeltaShot);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        rotationSpeed = GetPrivateStat(SkillRankItemType.BulletAimSpeed);
    }
    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        lightsaber = lightsaberPrefab.Spawn(GameManager.Instance.GameLoader.transform);
        lightsaber.Initialize(ship, percentDamage, rotationSpeed);
        PlayEffect();
        canAttack = true;
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        lightsaber.Recycle();
    }
    public override void Updating() {
        if (canAttack) {
            lightsaber.FollowShip();
        }
    }
    private void PlayEffect() {
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.DeltaShot), index);
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.DeltaShot));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({ GetNextStat(SkillRankItemType.DeltaShot)})</color>");
    }
}
