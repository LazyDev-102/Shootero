

using DG.Tweening;
using Helper;
using System.Collections;
using UnityEngine;
using Gemmob;

public class B01RageAttackComponent : BossAttackComponent {
    [SerializeField] private B01Attack bossAttack;
    [SerializeField] private float aimTime;
    [SerializeField] private float distanceMove1;
    [SerializeField] private AnimationCurve moveCuver1;
    [SerializeField] private float distanceMove2;
    [SerializeField] private AnimationCurve moveCuver2;
    [SerializeField] private ParticleSystem burstEffect;
    [SerializeField] private TrailRenderer[] trails;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;


    private bool activeAim;
    private bool isMoving;
    Tweener curTween;
    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B01Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B01Base.CurrentPhaseIndex];
        }
    }


    public override void StartAttack() {
        attackData = CurAttackData;
        activeAim = false;
        isMoving = false;
        bossAttack.BossBase.BossMove.EndMoveIdle();
        foreach (var t in trails) {
            t.gameObject.SetActive(true);
            t.HideTrail();
        }
    }

    public override void Updating() {
        if (activeAim) {
            bossAttack.B01Base.LookTarget();
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
        if (curTween != null) {
            curTween.Kill();
        }
        foreach (var t in trails) {
            t.HideTrail();
        }
    }

    private IEnumerator IDelayAttack() {
        activeAim = true;
        yield return Yielder.Wait(aimTime);
        activeAim = false;
        isMoving = true;
        curTween = bossAttack.BossBase.BossMove.StartMoveFront(transform.position + transform.up * distanceMove1, attackData.MoveSpeed1, moveCuver1, null);
        curTween.OnUpdate(() => {
            if (curTween.ElapsedPercentage() > 0.8f) {
                if (burstEffect) {
                    burstEffect.Play();
                }
                foreach (var t in trails) {
                    t.ShowTrail();
                }
                curTween.Kill();
                curTween = bossAttack.BossBase.BossMove.StartMoveFront(transform.position + transform.up * distanceMove2, attackData.MoveSpeed2, moveCuver2, null);
            }
        });
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float moveSpeed1;
        [SerializeField] private float moveSpeed2;

        public float MoveSpeed1 { get => moveSpeed1; }
        public float MoveSpeed2 { get => moveSpeed2; }
    }
}
