

using DG.Tweening;
using Gemmob;
using Helper;
using System.Collections;
using UnityEngine;

public class B14RageAttackComponent : BossAttackComponent {
    [SerializeField] private B14Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private float deltaTime;
    [SerializeField] private DOTweenAnimation rotation;
    [SerializeField] private AnimationCurve rageMoveCurve;
    [SerializeField] private B14Piece[] b14Pieces;
    [SerializeField] private Transform[] pieceDefaultPos;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;


    private AttackData attackData;
    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B14Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B14Base.CurrentPhaseIndex];
        }
    }

    public override void Attacking() {
        if (gameObject.activeInHierarchy)
            StartCoroutine(Moving());
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        bossAttack.SetLockEndAttack(true);
        bossAttack.StopShotAttack();
        StopAllCoroutines();
        bossAttack.B14Base.PierceCanHitDamage(false);
        rotation.DOPause();
        for (int i = 0; i < b14Pieces.Length; i++) {
            b14Pieces[i].TurnShield(true)
                        .TurnTrail(true);
        }
    }

    public override void Updating() {
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    private IEnumerator Moving() {
        yield return Yielder.Wait(delayAttack);
        for (int i = 0; i < b14Pieces.Length; i++) {
            b14Pieces[i].MoveAttack(bossAttack.Target, 50f / attackData.Speed);
            yield return Yielder.Wait(attackData.DeltaTime);
        }
        yield return Yielder.Wait(attackData.DeltaTime * 3);
        bossAttack.B14Base.B14Move.MoveRageAttack(bossAttack.Target, 50f / attackData.Speed, OnMoveComplete);
        //MoveAttack(bossAttack.Target, 20);
    }
    //public void MoveAttack(Transform target, float duration) {
    //    Vector3[] pathPoints = new Vector3[4];
    //    pathPoints[0] = transform.position;
    //    pathPoints[1] = transform.position + transform.up.normalized * 1.5f;
    //    pathPoints[2] = target.position;
    //    pathPoints[3] = target.position.y > 0 ? target.position + target.up.normalized * 15 : target.position + target.up.normalized * -15;
    //    transform.DOPath(pathPoints, duration, PathType.CatmullRom, PathMode.TopDown2D, 5).SetLookAt(0.01f, Vector3.forward, Vector3.right).OnComplete(OnMoveComplete).SetEase(rageMoveCurve);
    //}
    private void OnMoveComplete() {
        bossAttack.SetLockEndAttack(false);
        for (int i = 0; i < b14Pieces.Length; i++) {
            b14Pieces[i].transform.position = pieceDefaultPos[i].position;
            b14Pieces[i].transform.rotation = pieceDefaultPos[i].rotation;
        }
        rotation.DOPlay();
        var ranVector2 = new Vector2(0.5f, 1.3f);
        bossAttack.transform.position = bossAttack.B14Base.B14Move.GetPointMoveB14(ranVector2);
        var posDefault = new Vector2(0.5f, 0.8f);
        bossAttack.transform.DOMove(bossAttack.B14Base.B14Move.GetPointMoveB14(posDefault), 2f).OnComplete(EndAttack);
        //DOVirtual.DelayedCall(4f, EndAttack);
    }
    public override void EndAttack() {
        base.EndAttack();
        bossAttack.B14Base.PierceCanHitDamage(true);
        for (int i = 0; i < b14Pieces.Length; i++) {
            b14Pieces[i].TurnShield(false)
                        .TurnTrail(false);
        }
    }
    public override void StopAttack() {
        base.StopAttack();
        bossAttack.B14Base.PierceCanHitDamage(true);
        for (int i = 0; i < b14Pieces.Length; i++) {
            b14Pieces[i].TurnShield(false)
                        .TurnTrail(false);
        }
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float speed;
        [SerializeField] private float deltaTime;

        public float Speed { get => speed; }
        public float DeltaTime { get => deltaTime; }
    }
}
