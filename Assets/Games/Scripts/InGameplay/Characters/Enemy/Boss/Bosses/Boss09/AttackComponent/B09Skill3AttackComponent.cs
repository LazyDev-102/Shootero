using UnityEngine;
using System.Collections.Generic;
using Gemmob;
using DG.Tweening;
using System.Collections;

public class B09Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B09Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private float timeBackward = 0.5f;
    [SerializeField] private Transform shield;
    [SerializeField] private Collider2D boomCollider;
    [SerializeField] private ParticleSystem EffectBoom;
    [SerializeField] private ParticleSystem condenseBoom;
    [SerializeField] private AnimationCurve moveCuver;

    private int numberAttack;
    private bool attacking;
    private bool hasCondenseBoom;
    private Vector3 target;
    private int setSpeed;
    private Countdowner backwardCd = new Countdowner();
    private Countdowner delayAttackCd = new Countdowner();
    private Countdowner attackBoomCd = new Countdowner();
    private Countdowner delayPerAttack = new Countdowner();
    private Countdowner delayTarget = new Countdowner();
    private AttackData CurAttackData {
        get {
            return attackDatas[CurrentPhaseIndex];
        }
    }
    public override void StartAttack() {
        numberAttack = 0;
        attacking = false;
        boomCollider.enabled = false;
        setSpeed = 0;
        backwardCd.StartCountdown(CurAttackData.TimeBackward);
        delayAttackCd.StartCountdown(delayAttack);
        attackBoomCd.StartCountdown(CurAttackData.TimeAttackBoom);
        delayPerAttack.StartCountdown(CurAttackData.DelayPerAttack);
        delayTarget.StartCountdown(0.5f);
    }

    public override void Updating() {
        if (delayAttackCd.IsCountdowning()) {
            bossAttack.B09Base.LookTarget();
            delayAttackCd.Countdowning(Time.deltaTime);
        }
        else
        if (numberAttack < CurAttackData.AttackCount) {
            if (delayPerAttack.IsCountdowning()) {
                delayPerAttack.Countdowning(Time.deltaTime);
            }
            else {
                if (backwardCd.IsCountdowning()) {
                    if (setSpeed == 0) {
                        setSpeed = 1;
                        bossAttack.B09Base.B09Move.SetTargetMoveAttack(bossAttack.Target.position, CurAttackData.BackardSpeed);
                    }
                    bossAttack.B09Base.LookTarget();
                    bossAttack.B09Base.B09Move.MoveBack();
                    backwardCd.Countdowning(Time.deltaTime);
                }
                else {
                    if (!attacking) {
                        if (setSpeed == 1) {
                            setSpeed = 2;
                            bossAttack.B09Base.B09Move.SetTargetMoveAttack(bossAttack.Target.position, CurAttackData.MoveSpeed);
                            target = GameManager.Instance.GameLoader.Ship.transform.position;
                            delayTarget.StartCountdown(0.5f);
                            if (condenseBoom != null) {
                                condenseBoom.Stop(true, ParticleSystemStopBehavior.StopEmitting);
                                condenseBoom.Play();
                            }
                        }
                        if (delayTarget.IsTimeOut()) {
                            //bossAttack.B09Base.B09Move.MoveFront();
                            //attacking = System.Math.Abs(target.y - bossAttack.transform.position.y) <= 2 && Vector2.Distance(target, bossAttack.transform.position) <= 2;
                            bossAttack.BossBase.BossMove.StartMoveFront(target, CurAttackData.MoveSpeed, moveCuver, () => attacking = true);
                        }
                        else {
                            bossAttack.B09Base.B09Move.LookTarget(target);
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
                            if (numberAttack >= CurAttackData.AttackCount)
                                DOVirtual.DelayedCall(1f, () => EndAttack());
                            else {
                                delayPerAttack.StartCountdown(CurAttackData.DelayPerAttack);
                                backwardCd.StartCountdown(CurAttackData.TimeBackward);
                                attackBoomCd.StartCountdown(CurAttackData.TimeAttackBoom);
                                attacking = false;
                                //hasCondenseBoom = false;
                                boomCollider.enabled = false;
                                setSpeed = 0;
                            }
                        }
                        else {
                            //if (condenseBoom != null && !hasCondenseBoom) {
                            //    hasCondenseBoom = true;
                            //    EffectBoom.Stop();
                            //    condenseBoom.Stop();
                            //    condenseBoom.time = 0;
                            //    condenseBoom.Play();
                            //}
                            attackBoomCd.Countdowning(Time.deltaTime);
                        }
                    }
                }
            }
        }
    }

    //public override void Updating() {
    //    if (delayAttackCd.IsCountdowning()) {
    //        bossAttack.B09Base.B09Move.LookTarget(bossAttack.Target.position);
    //        delayAttackCd.Countdowning(Time.deltaTime);
    //    }
    //    else
    //    if(numberAttack < CurAttackData.AttackCount) {
    //        if (delayPerAttack.IsCountdowning()) {
    //            delayPerAttack.Countdowning(Time.deltaTime);
    //        } else {
    //            if (backwardCd.IsCountdowning()) {
    //                if (setSpeed == 0) {
    //                    setSpeed = 1;
    //                    bossAttack.B09Base.B09Move.SetTargetMoveAttack(bossAttack.Target.position, CurAttackData.BackardSpeed);
    //                }
    //                bossAttack.B09Base.B09Move.LookTarget(bossAttack.Target.position);
    //                bossAttack.B09Base.B09Move.MoveBack();
    //                backwardCd.Countdowning(Time.deltaTime);
    //            }
    //            else {
    //                if(!attacking) {
    //                    if(setSpeed == 1) {
    //                        setSpeed = 2;
    //                        bossAttack.B09Base.B09Move.SetTargetMoveAttack(bossAttack.Target.position, CurAttackData.MoveSpeed);
    //                        target = GameManager.Instance.GameLoader.Ship.transform.position;
    //                        delayTarget.StartCountdown(0.5f);
    //                    }
    //                    if (delayTarget.IsTimeOut()) {
    //                        bossAttack.B09Base.B09Move.MoveFront();
    //                        attacking = System.Math.Abs(target.y - bossAttack.transform.position.y) <= 2 && Vector2.Distance(target, bossAttack.transform.position) <= 2;
    //                    } else {
    //                        bossAttack.B09Base.B09Move.LookTarget(target);
    //                        delayTarget.Countdowning(Time.deltaTime);
    //                    }
    //                } else {
    //                    if (attackBoomCd.IsTimeOut()) {
    //                        boomCollider.enabled = true;
    //                        if(EffectBoom != null){
    //                            condenseBoom.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    //                            EffectBoom.Stop();
    //                            EffectBoom.time = 0;
    //                            EffectBoom.Play();
    //                        }

    //                        numberAttack++; 
    //                        if(numberAttack >= CurAttackData.AttackCount) DOVirtual.DelayedCall(1f,()=> EndAttack());
    //                        else {
    //                            delayPerAttack.StartCountdown(CurAttackData.DelayPerAttack);
    //                            backwardCd.StartCountdown(CurAttackData.TimeBackward);
    //                            attackBoomCd.StartCountdown(CurAttackData.TimeAttackBoom);
    //                            attacking = false;
    //                            hasCondenseBoom = false;
    //                            boomCollider.enabled = false;
    //                            setSpeed = 0;
    //                        }
    //                    } else {
    //                        if(condenseBoom != null && !hasCondenseBoom) {
    //                            hasCondenseBoom = true;
    //                            EffectBoom.Stop();
    //                            condenseBoom.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    //                            condenseBoom.time = 0;
    //                            condenseBoom.Play();
    //                        }
    //                        attackBoomCd.Countdowning(Time.deltaTime);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}


    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void Attacking() {
        //ActiveShield(true);
    }

    public override void EndAttack() {
        base.EndAttack();
        //ActiveShield(false);
        boomCollider.enabled = false;
        if (EffectBoom != null)
            EffectBoom.Stop();
    }


    private void ActiveShield(bool active) {
        shield.gameObject.SetActive(active);
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
        [SerializeField, Tooltip("Sau khi đến đích chờ bao lâu để Play Effect tỏa")] private float timeAttackBoom;

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
