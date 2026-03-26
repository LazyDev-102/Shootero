

using DG.Tweening;
using Gemmob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B12RageAttackComponent : BossAttackComponent {
    [SerializeField] private B12Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Vector2 originPos = Vector2.one * 0.5f;
    [SerializeField] private ParticleSystem blackHoleEffect;
    [SerializeField] private B12ChildBase meteoritePrefab;
    [SerializeField] private AttackData[] attackDatas;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    private AttackData attackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackDatas[bossAttack.B12Base.CurrentPhaseIndex];
            else
                return bossModeAttackDatas[bossAttack.B12Base.CurrentPhaseIndex];
        }
    }


    private readonly Vector2 spawnPosition = new Vector2(30, 30);
    private Countdowner delayCD = new Countdowner();
    private List<B12ChildBase> b12ChildBases;
    private bool canAttack;

    public override void PreloadIngame() {
        if (meteoritePrefab) {
            meteoritePrefab.PreloadIngame();
            meteoritePrefab.RegisterPool(20);
        }
    }

    public override void Initialize() {
        b12ChildBases = new List<B12ChildBase>();
        base.Initialize();
    }
    public override void Attacking() {
    }

    public override void StartAttack() {
        attackData = CurAttackData;
        delayCD.StartCountdown(delayAttack);
        bossAttack.B12Base.B12Move.StartMoveAfterAttackB12(originPos);
        bossAttack.B12Base.SetCanSpecialAttack(false);
        bossAttack.B12Base.ClearEnemyChild();
        blackHoleEffect.transform.localScale = Vector3.one;
        blackHoleEffect.gameObject.SetActive(true);
    }

    public override void Updating() {
        if (delayCD.IsCountdowning()) {
            delayCD.Countdowning(Time.deltaTime);
            canAttack = bossAttack.B12Base.B12Move.CompleteMoveToTarget();
        }
        else if (!canAttack) {
            delayCD.Countdowning(Time.deltaTime);
        }
        else {
            canAttack = false;
            bossAttack.transform.position = spawnPosition;
            //Boss Bien mat
            //Play Effect
            if (blackHoleEffect != null) {
                blackHoleEffect.transform.position = bossAttack.B12Base.B12Move.GetPointMoveB12(originPos);
                blackHoleEffect.Play();
            }
            //Attack
            if (gameObject.activeInHierarchy)
                StartCoroutine(SpawnMeteorite(blackHoleEffect));
        }
    }

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    public override void EndAttack() {
        OnEndAttack();
        base.EndAttack();
    }
    private void OnEndAttack() {
        foreach (var item in b12ChildBases) {
            if (item != null) {
                item.Recycle();
            }
        }
        if (gameObject.activeInHierarchy)
            DOVirtual.DelayedCall(2f, () => bossAttack.B12Base.SetCanSpecialAttack(true));
        b12ChildBases.Clear();
    }

    private IEnumerator SpawnMeteorite(ParticleSystem blackHole) {
        yield return Yielder.Wait(1f);
        //var targetPos = bossAttack.B12Base.B12Move.GetPointMoveB12(originPos);
        for (int i = 0; i < attackData.NumberMeteorite; i++) {
            Vector2 spawnPosition4 = new Vector2(Random.Range(0, -10), Random.Range(15, 25));
            Vector2 spawnPosition1 = new Vector2(Random.Range(-2, -2), Random.Range(-15, -25));
            Vector2 spawnPosition2 = new Vector2(Random.Range(-2, -2), Random.Range(15, 25));
            Vector2 spawnPosition3 = new Vector2(Random.Range(0, 10), Random.Range(-15, -25));
            var ran = Random.Range(0, 4);
            var bClone = meteoritePrefab.Spawn(GameLoader.transform, ran == 0 ? spawnPosition : ran == 1 ? spawnPosition1 : ran == 2 ? spawnPosition3 : spawnPosition4);
            b12ChildBases.Add(bClone);
            bClone.B12ChildMove.StartMove(blackHoleEffect.transform.position, attackData.TimeMove, () => MeteoriteMoveComplete(bClone));
            yield return Yielder.Wait(attackData.TimePerShot);
        }
        yield return Yielder.Wait(2f);
        blackHoleEffect.transform.DOScale(0, 1f).SetEase(Ease.Linear);
        yield return Yielder.Wait(1f);
        blackHoleEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        blackHoleEffect.gameObject.SetActive(false);
        EndAttack();
    }

    private void MeteoriteMoveComplete(B12ChildBase meteorite) {
        meteorite.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.Linear).OnComplete(() => {
            meteorite.Recycle();
        });
    }
    [System.Serializable]
    private class AttackData {
        [SerializeField] private float timePerShot;
        [SerializeField] private float timeMove;
        [SerializeField] private int numberMeteorite;

        public float TimePerShot { get => timePerShot; }
        public float TimeMove { get => timeMove; }
        public int NumberMeteorite { get => numberMeteorite; }
    }
}
