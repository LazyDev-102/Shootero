using Gemmob;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRegeneration", menuName = "Resource/HardData/Skill/SkillRegeneration")]
public class SkillRegeneration : ItemSkillData {

    private float duration = 10f;
    private float percentHeal = 1f;
    private float deltaTime = 1f;

    private Countdowner durationCd = new Countdowner();
    private Countdowner deltaCd = new Countdowner();
    private void SetStatData() {
        deltaTime = GetStat(SkillRankItemType.DeltaShot);
        percentHeal = GetStat(SkillRankItemType.PercentHp) * 100;
        duration = GetStat(SkillRankItemType.Duration);
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        Healing();
        durationCd.StartCountdown(duration);
        canAttack = true;
    }
    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        canAttack = false;
    }
    public override void Updating() {
        if (canAttack) {
            if (durationCd.IsTimeOut()) {
                EndAttack(ship);
            }
            else {
                if (deltaCd.IsTimeOut()) {
                    Healing();
                }
                deltaCd.Countdowning(Time.deltaTime);
            }
            durationCd.Countdowning(Time.deltaTime);
        }
    }
    private void Healing() {
        PlayEffect();
        ship.ShipHealth.AddHpByPercentWithHealing(percentHeal / 100f);
        deltaCd.StartCountdown(deltaTime);

    }
    private void PlayEffect() {
        IngameHUD.Instance.Combat.OnPlayerHealthUp();
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentHp, index) * 100,
                            GetStat(SkillRankItemType.DeltaShot, index),
                            GetStat(SkillRankItemType.Duration, index),
                            GetStat(SkillRankItemType.CoolDown, index));
    }
    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentHp) * 100,
                            GetStat(SkillRankItemType.DeltaShot),
                            GetStat(SkillRankItemType.Duration),
                            GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.PercentHp) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentHp) * 100})</color>",
                            $"{GetStat(SkillRankItemType.DeltaShot)}<color=green>({GetNextStat(SkillRankItemType.DeltaShot)})</color>",
                            $"{GetStat(SkillRankItemType.Duration)}<color=green>({GetNextStat(SkillRankItemType.Duration)})</color>",
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
}
