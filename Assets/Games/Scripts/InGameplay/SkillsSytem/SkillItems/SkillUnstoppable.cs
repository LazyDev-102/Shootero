
using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillUnstoppable", menuName = "Resource/HardData/Skill/SkillUnstoppable")]
public class SkillUnstoppable : ItemSkillData {
    [SerializeField] private ParticleSystem effect;

    private ParticleSystem effectClone;
    private float duration = 10f;
    private Countdowner durationCd = new Countdowner();

    public override void Preload() {
        if (effect != null) {
            effect.RegisterPool(1);
        }
    }

    private void SetStatData() {
        duration = GetStat(SkillRankItemType.Duration);
    }

    public override bool IsReady(ShipBase ship) {
        return ship.ShipAttack.PatternType != ShotPatternType.Basic;
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ship.ShipHitbox.TurnOnProtectShield(duration);
        ship.ShipHitbox.SetLockTurnOffStatus(true);
        ship.ShipAttack.SetForceMaxLevelBulletUp(true);
        durationCd.StartCountdown(duration);
        canAttack = true;
        PlayEffect();
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        ship.ShipHitbox.SetLockTurnOffStatus(false);
        ship.ShipHitbox.TurnOffProtectShield();
        ship.ShipAttack.SetForceMaxLevelBulletUp(false);
        DestroyEffect();
        canAttack = false;
    }
    public override void Updating() {
        if (canAttack) {
            if (durationCd.IsTimeOut()) {
                EndAttack(ship);
            }
            durationCd.Countdowning(Time.deltaTime);
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
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration, index),
                            GetStat(SkillRankItemType.CoolDown, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.Duration),
                            GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.Duration)}<color=green>({GetNextStat(SkillRankItemType.Duration)})</color>",
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
}
