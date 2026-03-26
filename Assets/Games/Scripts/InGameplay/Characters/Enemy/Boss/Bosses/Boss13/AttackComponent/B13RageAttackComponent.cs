
using DG.Tweening;
using Gemmob;
using System;
using System.Collections;
using UnityEngine;

public class B13RageAttackComponent : BossSkillAttackComponent {
    [SerializeField] private B13Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private T01Base iceBullet;
    [SerializeField] private T01Base firebullet;
    [SerializeField] private int numberPreload;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private bool attacking;
    private AttackData attackData;
    private Countdowner delayCountdowner = new Countdowner();
    private Countdowner durationCD = new Countdowner();
    private Countdowner deltaShotCD = new Countdowner();
    private readonly Vector2 spawnPosition = new Vector2(20, 20);
    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }

    public override void PreloadIngame() {
        if (iceBullet) {
            iceBullet.PreloadIngame();
            iceBullet.RegisterPool(numberPreload);
        }
        if (firebullet) {
            firebullet.PreloadIngame();
            firebullet.RegisterPool(numberPreload);
        }
    }
    public override void Attacking() {
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        durationCD.StartCountdown(attackData.TimeAttack);
        deltaShotCD.StartCountdown(attackData.TimePerShot);
        delayCountdowner.StartCountdown(delayAttack);
        bossAttack.B13Base.B13Move.StartMoveAfterAttackB13(new Vector2(0.5f, 0.5f));
    }

    public override void Updating() {
        bossAttack.B13Base.B13Move.LookTarget(bossAttack.Target.position);
        if (!attacking) {
            attacking = bossAttack.B13Base.B13Move.CompleteMoveToTarget();
        }
        else {
            if (delayCountdowner.IsCountdowning()) {
                delayCountdowner.Countdowning(Time.deltaTime);
            }
            else {
                if (durationCD.IsCountdowning()) {
                    durationCD.Countdowning(Time.deltaTime);
                    if (deltaShotCD.IsTimeOut()) {
                        Shot();
                        deltaShotCD.StartCountdown(attackData.TimePerShot);
                    }
                    else {
                        deltaShotCD.Countdowning(Time.deltaTime);
                    }
                }
                else {
                    EndAttack();
                }
            }
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }


    private void Shot() {
        var clone = GameLoader.SpawnTrap(UnityEngine.Random.Range(0, 2) == 0 ? iceBullet : firebullet, spawnPosition);
        clone.Initialize();
        clone.T01Stat.MoveSpeed.SetBaseValue(attackData.Speed);
        clone.T01Stat.Atk.SetBaseValue((int)(bossAttack.B13Base.B13Stat.Atk.Value * attackData.DamagePercent));
        clone.gameObject.SetActive(true);
    }
    public override void EndAttack() {
        base.EndAttack();
    }

    public override void StopAttack() {
        base.StopAttack();
    }


    [Serializable]
    private class AttackData {
        [SerializeField] private float totalTimeAttack;
        [SerializeField] private float timePerShot;
        [SerializeField] private float speed;
        [SerializeField] private float damagePercent;

        public float TimeAttack {
            get => totalTimeAttack;
        }
        public float TimePerShot {
            get => timePerShot;
        }
        public float Speed { get => speed; }
        public float DamagePercent { get => damagePercent; }
    }
}
