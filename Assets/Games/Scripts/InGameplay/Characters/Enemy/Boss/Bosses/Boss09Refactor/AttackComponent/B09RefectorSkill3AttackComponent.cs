
using UnityEngine;
using DG.Tweening;

public class B09RefectorSkill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B09RefectorAttack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float timeBackward = 0.5f;
    [SerializeField] private Transform shield;
    [SerializeField] private Collider2D boomCollider;
    [SerializeField] private ParticleSystem EffectBoom;
    [SerializeField] private ParticleSystem condenseBoom;
    [SerializeField] private AnimationCurve moveCuver;

    private int numberAttack;
    private bool attacking;
    private Vector3 target;
    private int setSpeed;
    private AttackData attackData;
    private Countdowner backwardCd = new Countdowner();
    private Countdowner delayAttackCd = new Countdowner();
    private Countdowner attackBoomCd = new Countdowner();
    private Countdowner delayPerAttack = new Countdowner();
    private Countdowner delayTarget = new Countdowner();
    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }
    public override void StartAttack() {
        attackData = CurAttackData;
        numberAttack = 0;
        attacking = false;
        boomCollider.enabled = false;
        setSpeed = 0;
        backwardCd.StartCountdown(attackData.TimeBackward);
        delayAttackCd.StartCountdown(delayAttack);
        attackBoomCd.StartCountdown(attackData.TimeAttackBoom);
        delayPerAttack.StartCountdown(attackData.DelayPerAttack);
        delayTarget.StartCountdown(0.5f);
    }

    public override void Updating() {
        if (delayAttackCd.IsCountdowning()) {
            bossAttack.B09RefectorBase.LookTarget();
            delayAttackCd.Countdowning(Time.deltaTime);
        }
        else
        if (numberAttack < attackData.AttackCount) {
            if (delayPerAttack.IsCountdowning()) {
                delayPerAttack.Countdowning(Time.deltaTime);
            }
            else {
                if (backwardCd.IsCountdowning()) {
                    if (setSpeed == 0) {
                        setSpeed = 1;
                        bossAttack.B09RefectorBase.B09RefectorMove.SetTargetMoveAttack(bossAttack.Target.position, attackData.BackardSpeed);
                    }
                    bossAttack.B09RefectorBase.LookTarget();
                    bossAttack.B09RefectorBase.B09RefectorMove.MoveBack();
                    backwardCd.Countdowning(Time.deltaTime);
                }
                else {
                    if (!attacking) {
                        if (setSpeed == 1) {
                            setSpeed = 2;
                            bossAttack.B09RefectorBase.B09RefectorMove.SetTargetMoveAttack(bossAttack.Target.position, attackData.MoveSpeed);
                            target = GameManager.Instance.GameLoader.Ship.transform.position;
                            delayTarget.StartCountdown(0.5f);
                            if (condenseBoom != null) {
                                condenseBoom.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                                condenseBoom.Play();
                            }
                        }
                        if (delayTarget.IsTimeOut()) {
                            bossAttack.BossBase.BossMove.StartMoveFront(target, attackData.MoveSpeed, moveCuver, () => attacking = true);
                        }
                        else {
                            bossAttack.B09RefectorBase.B09RefectorMove.LookTarget(target);
                            delayTarget.Countdowning(Time.deltaTime);
                        }
                    }
                    else {
                        if (attackBoomCd.IsTimeOut()) {
                            boomCollider.enabled = true;
                            if (EffectBoom != null) {
                                condenseBoom.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                                EffectBoom.Stop();
                                EffectBoom.time = 0;
                                EffectBoom.Play();
                            }

                            numberAttack++;
                            if (numberAttack >= attackData.AttackCount)
                                DOVirtual.DelayedCall(1f, () => EndAttack());
                            else {
                                delayPerAttack.StartCountdown(attackData.DelayPerAttack);
                                backwardCd.StartCountdown(attackData.TimeBackward);
                                attackBoomCd.StartCountdown(attackData.TimeAttackBoom);
                                attacking = false;
                                boomCollider.enabled = false;
                                setSpeed = 0;
                            }
                        }
                        else {
                            attackBoomCd.Countdowning(Time.deltaTime);
                        }
                    }
                }
            }
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void Attacking() {
    }

    public override void EndAttack() {
        base.EndAttack();
        boomCollider.enabled = false;
        if (EffectBoom != null)
            EffectBoom.Stop();
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private int attackCount;
        [SerializeField] private int delayPerAttack;
        [SerializeField] private int moveSpeed;
        [SerializeField] private int backardSpeed;
        [SerializeField] private float damagePercent;
        [SerializeField] private float radiusSpread;
        [SerializeField] private float distanceBackward;
        [SerializeField] private float timeBackward;
        [SerializeField] private float timeAttackBoom;

        public int AttackCount { get => attackCount; }
        public int DelayPerAttack { get => delayPerAttack; }
        public int MoveSpeed { get => moveSpeed; }
        public int BackardSpeed { get => backardSpeed; }
        public float DamagePercent { get => damagePercent; }
        public float RadiusSpread { get => radiusSpread; }
        public float DistanceBackward { get => distanceBackward; }
        public float TimeBackward { get => timeBackward; }
        public float TimeAttackBoom { get => timeAttackBoom; }

    }
}
