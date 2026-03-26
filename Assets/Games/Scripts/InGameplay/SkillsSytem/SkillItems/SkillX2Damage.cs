using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillX2Damage", menuName = "Resource/HardData/Skill/SkillX2Damage")]
public class SkillX2Damage : ItemSkillData {
    [SerializeField] private StatModifier damagePercent;
    [SerializeField] private ParticleSystem effect;

    private float duration;
    private ParticleSystem effectClone;

    private Countdowner cd = new Countdowner();

    private void SetStatData() {
        duration = GetStat(SkillRankItemType.Duration);
    }

    public override void Preload() {
        if (effect != null)
            effect.RegisterPool(1);
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ship.ShipStat.Atk.AddModifier(damagePercent);
        cd.StartCountdown(duration);
        canAttack = true;
        PlayEffect();
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        ship.ShipStat.Atk.RemoveModifier(damagePercent);
        DestroyEffect();
        canAttack = false;
    }
    public override void Updating() {
        if (canAttack) {
            if (cd.IsTimeOut()) {
                EndAttack(ship);
            }
            cd.Countdowning(Time.deltaTime);
        }
    }
    private void PlayEffect() {
        if (effect != null) {
            effectClone = effect.Spawn(ship.transform, ship.transform.position);
        }
    }
    private void DestroyEffect() {
        if (effectClone != null) {
            effectClone.Recycle();
        }
    }
    public override string GetDescriptionByIndex(int rank) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration, rank),
                            GetStat(SkillRankItemType.CoolDown), rank);
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration),
                            GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.Duration)}<color=green>({ GetNextStat(SkillRankItemType.Duration)})</color>",
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({ GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
}
