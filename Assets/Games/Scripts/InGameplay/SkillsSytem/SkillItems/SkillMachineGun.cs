
using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillMachineGun", menuName = "Resource/HardData/Skill/SkillMachineGun")]
public class SkillMachineGun : ItemSkillData {
    [SerializeField] private MachineGun gunPrefab;
    [SerializeField] private float aimTime = 0.5f;
    [SerializeField] private Explosioner effect;

    private int numberGun = 5;
    private float duration = 20f;
    private float deltaShot = 0.5f;
    private float percentDamage = 1;
    private int bulletSpeed = 10;
    private int aimSpeed = 20;
    private List<MachineGun> guns = new List<MachineGun>();
    private Countdowner durationCd = new Countdowner();

    private void SetStatData() {
        duration = GetStat(SkillRankItemType.Duration);
        percentDamage = GetStat(SkillRankItemType.PercentDamage);
        deltaShot = GetPrivateStat(SkillRankItemType.DeltaShot);
        numberGun = (int)GetPrivateStat(SkillRankItemType.BulletCount);
        bulletSpeed = (int)GetPrivateStat(SkillRankItemType.BulletSpeed);
        aimSpeed = (int)GetPrivateStat(SkillRankItemType.BulletAimSpeed);
    }

    public override void StartAttack(ShipBase ship) {
        base.StartAttack(ship);
        SetStatData();
        ResetData();
        durationCd.StartCountdown(duration);
        Shot();
        PlayEffect();
        canAttack = true;
    }

    public override void EndAttack(ShipBase ship) {
        base.EndAttack(ship);
        canAttack = false;
        ResetData();
    }

    private void Shot() {
        for (int i = 0; i < numberGun; i++) {
            var pos = BorderHelper.GetRandomPointBottomBorder(0.17f * (i + 1));
            var bGun = gunPrefab.Spawn(pos);
            bGun.StartAttack((int)(ship.ShipStat.Atk.Value * percentDamage), deltaShot, bulletSpeed, aimTime, aimSpeed);
            guns.Add(bGun);
        }
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
        if (effect != null)
            effect.Spawn(ship.transform.position);
    }

    public override void ResetData() {
        for (int i = 0; i < guns.Count; i++) {
            if (guns[i] != null) {
                guns[i].EndAttack();
                guns[i].Recycle();
            }
        }
        guns.Clear();
    }
    public override void Preload() {
        if (effect != null)
            effect.RegisterPool(1);
        if (gunPrefab != null)
            gunPrefab.Preload();
    }
    public override string GetDescriptionByIndex(int index) {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage, index) * 100,
                            GetStat(SkillRankItemType.Duration, index),
                            GetStat(SkillRankItemType.CoolDown, index));
    }

    protected override string GetCurrentDescription() {
        return string.Format(Description,
                            GetStat(SkillRankItemType.PercentDamage) * 100,
                            GetStat(SkillRankItemType.Duration),
                            GetStat(SkillRankItemType.CoolDown));
    }
    protected override string GetNextDescription() {
        return string.Format(Description,
                            $"{GetStat(SkillRankItemType.PercentDamage) * 100}<color=green>({GetNextStat(SkillRankItemType.PercentDamage) * 100})</color>",
                            $"{GetStat(SkillRankItemType.Duration)}<color=green>({GetNextStat(SkillRankItemType.Duration)})</color>",
                            $"{GetStat(SkillRankItemType.CoolDown)}<color=green>({GetNextStat(SkillRankItemType.CoolDown)})</color>");
    }
}
