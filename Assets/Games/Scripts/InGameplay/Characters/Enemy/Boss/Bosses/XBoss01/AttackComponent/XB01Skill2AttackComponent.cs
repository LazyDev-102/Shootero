using UnityEngine;
using System;
using Gemmob;
using System.Collections;
using DG.Tweening;

public class XB01Skill2AttackComponent : BossSkillAttackComponent {
    [SerializeField] private XB01Attack bossAttack;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform shield;
    [SerializeField] private ParticleSystem burstEffect;
    [SerializeField] private GameObject trail1;
    [SerializeField] private GameObject trail2;

    private bool activeAim;
    private bool isMoving;
    private bool playEffect;
    private int numberAttack = 0;
    private AttackData attackData;

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
        activeAim = false;
        isMoving = false;
        playEffect = false;
        numberAttack = 0;
        bossAttack.XB01Base.XB01Move.StopMoveIdle();
        trail1.SetActive(true);
        trail2.SetActive(true);
    }
    public override void Updating() {
        if (activeAim) {
            bossAttack.XB01Base.LookTarget();
        }
        if (isMoving) {
            bossAttack.XB01Base.XB01Move.MoveFront();
            if (burstEffect != null && !playEffect) {
                playEffect = true;
                burstEffect.Play();
            }
            if (bossAttack.XB01Base.XB01Move.HasOutBorder()) {
                isMoving = false;
                numberAttack++;
                var ranVector2 = new Vector2(0.5f, 1.1f);
                bossAttack.transform.position = bossAttack.XB01Base.XB01Move.GetPointMoveXB01(ranVector2);
                ChangeZ(bossAttack.transform);
                if (numberAttack < attackData.AttackCount) {
                    activeAim = true;
                    DOVirtual.DelayedCall(0.1f, () => { activeAim = false; isMoving = true; });
                }
                else {
                    var posDefault = new Vector2(0.5f, 0.8f);
                    bossAttack.transform.DOMove(bossAttack.XB01Base.XB01Move.GetPointMoveXB01(posDefault), 2f).OnComplete(() => EndAttack());
                }
            }
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(IDelayAttack());
    }

    public override void EndAttack() {
        base.EndAttack();
        trail1.SetActive(false);
        trail2.SetActive(false);
    }

    private IEnumerator IDelayAttack() {
        activeAim = true;
        yield return Yielder.Wait(attackData.AimTime);
        activeAim = false;
        isMoving = true;
        bossAttack.XB01Base.XB01Move.SetTargetMoveAttack(bossAttack.Target.position, attackData.MoveSpeed);
    }

    private void ChangeZ(Transform trans) {
        var temp = trans.rotation;
        temp.z = 180;
        trans.rotation = temp;
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float attackCount;
        [SerializeField] private float aimTime;

        public float DamagePercent {
            get => damagePercent;
        }
        public float MoveSpeed {
            get => moveSpeed;
        }
        public float AttackCount {
            get => attackCount;
        }
        public float AimTime {
            get => aimTime;
        }
    }
}
