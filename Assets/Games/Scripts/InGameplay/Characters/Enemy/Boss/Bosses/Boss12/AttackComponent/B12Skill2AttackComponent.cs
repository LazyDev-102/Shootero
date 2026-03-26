using UnityEngine;
using System;
using Gemmob;

public class B12Skill2AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B12Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private T01Base bullet;
    [SerializeField] private int numberPreload;

    AttackData attackData;
    private Countdowner durationCD = new Countdowner();
    private Countdowner deltaShotCD = new Countdowner();
    private Countdowner delayCD = new Countdowner();
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
        if (bullet) {
            bullet.PreloadIngame();
            bullet.RegisterPool(numberPreload);
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void Initialize() {
        base.Initialize();
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        durationCD.StartCountdown(attackData.TimeAttack);
        deltaShotCD.StartCountdown(attackData.TimePerShot);
        delayCD.StartCountdown(delayAttack);
    }

    public override void Attacking() {
    }

    private void Shot() {
        if (GameLoader == null || bullet == null)
            return;
        var clone = GameLoader.SpawnTrap(bullet, spawnPosition);
        clone.Initialize();
        clone.gameObject.SetActive(true);
        //bullet.Spawn(GameLoader.transform, spawnPosition);
    }
    public override void Updating() {
        bossAttack.B12Base.B12Move.LookTarget(bossAttack.Target.position);
        if (delayCD.IsCountdowning()) {
            delayCD.Countdowning(Time.deltaTime);
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

        public float TimeAttack {
            get => totalTimeAttack;
        }
        public float TimePerShot {
            get => timePerShot;
        }
    }
}
