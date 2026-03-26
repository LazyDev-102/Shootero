//using Boo.Lang;
using DG.Tweening;
using UnityEngine;

public class B13Skill1AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B13Attack bossAttack;
    [SerializeField] private float delayAttack;
    [SerializeField] private Transform fireBossTrans;
    [SerializeField] private Transform iceBossTrans;
    [SerializeField] private Transform fireDefaultPos;
    [SerializeField] private Transform iceDefaultPos;
    [SerializeField] private Transform firePoint;
    [SerializeField] private B13T02Base[] bullet;
    [SerializeField] private int[] attackCount;
    [SerializeField] private float[] damagePercent;
    [SerializeField] private float[] bossModeDamagePercent;

    private Countdowner delayCountdowner = new Countdowner();
    private Countdowner endCountdowner = new Countdowner();
    private bool hasSpawn;
    private int currentNumberAttack;
    private int maxNumberAttack;
    private System.Collections.Generic.List<B13T02Base> bullets = new System.Collections.Generic.List<B13T02Base>();
    private Tweener boss1Tweener;
    private Tweener boss2Tweener;

    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }
    private void MoveOut() {
        bossAttack.B13Base.B13Move.MoveInside(false);
        bossAttack.B13Base.B13Hitbox.ActiveCollider(false);

        if (boss1Tweener != null)
            boss1Tweener.Kill();
        if (boss2Tweener != null)
            boss2Tweener.Kill();
        boss1Tweener = fireBossTrans.DOMoveX(transform.position.x - 100, 5f);
        boss2Tweener = iceBossTrans.DOMoveX(transform.position.x + 100, 5f);
    }
    private void MoveIn() {
        if (boss1Tweener != null)
            boss1Tweener.Kill();
        if (boss2Tweener != null)
            boss2Tweener.Kill();
        boss1Tweener = fireBossTrans.DOMove(fireDefaultPos.position, 1f);
        boss2Tweener = iceBossTrans.DOMove(iceDefaultPos.position, 1f).OnComplete(() => {
            bossAttack.B13Base.B13Hitbox.ActiveCollider(true);
            bossAttack.B13Base.B13Move.MoveInside(true);
            EndAttack();
        });
    }

    public override void Updating() {
        if (delayCountdowner.IsCountdowning()) {
            delayCountdowner.Countdowning(Time.deltaTime);
            bossAttack.B13Base.B13Move.LookTarget(bossAttack.Target.position);
        }
        else {
            if (hasSpawn) {
                if (endCountdowner.IsCountdowning()) {
                    endCountdowner.Countdowning(Time.deltaTime);
                }
                else {
                    if (currentNumberAttack == maxNumberAttack) {
                        delayCountdowner.StartCountdown(10);
                        MoveIn();
                    }
                    else {
                        currentNumberAttack++;
                        endCountdowner.StartCountdown(5);
                        hasSpawn = false;
                    }
                }
            }
            else {
                if (bullet[CurrentPhaseIndex] == null || bossAttack == null && bossAttack.B13Base == null)
                    return;
                var bClone = GameLoader.SpawnTrap(bullet[CurrentPhaseIndex], GameLoader.transform.position);
                var damage = IngameData.currentGameMode == GameMode.EventBoss ? bossModeDamagePercent[CurrentPhaseIndex] : damagePercent[CurrentPhaseIndex];
                bClone.Initialize();
                bClone.ChangedStatWithMultipler(damage);
                bClone.SetOwner(bossAttack.B13Base);
                bClone.gameObject.SetActive(true);
                hasSpawn = true;
                bullets.Add(bClone);
            }
        }
    }

    public override void StartAttack() {
        hasSpawn = false;
        maxNumberAttack = attackCount[CurrentPhaseIndex];
        currentNumberAttack = 1;
        delayCountdowner.StartCountdown(delayAttack);
        endCountdowner.StartCountdown(5);
        MoveOut();
    }
    private void ResetState() {
        foreach (var trap in bullets) {
            if (trap != null)
                GameLoader.DespawnTrap(trap);
        }
        bullets.Clear();
        if (boss1Tweener != null)
            boss1Tweener.Kill();
        if (boss2Tweener != null)
            boss2Tweener.Kill();
    }
    public override void BossDestroy() {
        ResetState();
        base.BossDestroy();
    }
    public override void Attacking() {

    }

    public override void EndAttack() {
        ResetState();
        base.EndAttack();
    }

    public override void StopAttack() {
        ResetState();
        base.StopAttack();
    }
}
