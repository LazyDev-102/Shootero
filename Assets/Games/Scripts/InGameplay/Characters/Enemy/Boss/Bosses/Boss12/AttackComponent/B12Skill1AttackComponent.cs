using UnityEngine;
using System;
using Gemmob;

public class B12Skill1AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B12Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private T02Base[] bullet;
    [SerializeField] private float[] damagePercent;
    [SerializeField] private float[] bossModeDamagePercent;

    private Countdowner delayCountdowner = new Countdowner();
    private Countdowner endCountdowner = new Countdowner();
    private bool hasSpawn;
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Initialize() {
        base.Initialize();
    }
    public override void Updating() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            bossAttack.B12Base.B12Move.LookTarget(bossAttack.Target.position);
        }
        else {
            if (hasSpawn) {
                if (endCountdowner.IsCountdowning()) {
                    endCountdowner.Countdowning(Time.deltaTime);
                }
                else {
                    EndAttack();
                }
            }
            else {
                var bClone = GameLoader.SpawnTrap(bullet[CurrentPhaseIndex], GameLoader.transform.position);
                var damage = IngameData.currentGameMode == GameMode.EventBoss ? bossModeDamagePercent[CurrentPhaseIndex] : damagePercent[CurrentPhaseIndex];
                bClone.Initialize();
                bClone.ChangedStatWithMultipler(damage);
                bClone.gameObject.SetActive(true);
                hasSpawn = true;
            }
        }
    }

    public override void StartAttack() {
        hasSpawn = false;
        delayCountdowner.StartCountdown(delayAttack);
        endCountdowner.StartCountdown(0.5f);
    }

    public override void Attacking() {

    }

    public override void EndAttack() {
        base.EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
    }
}
