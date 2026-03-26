using DG.Tweening;
using UnityEngine;

public class B13Skill3AttackComponent : BossSkillAttackComponent {
    [SerializeField] private B13Attack bossAttack;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Transform bossFire;
    [SerializeField] private Transform bossIce;
    [SerializeField] private SinBullet leftBullet;
    [SerializeField] private SinBullet rightBullet;
    [SerializeField] private float delayAttack;
    [SerializeField] private AnimationCurve moveCuver1;
    [SerializeField] private AnimationCurve moveCuver2;
    [SerializeField] private AttackData[] attackData;
    [SerializeField] private AttackData[] bossModeAttackDatas;

    Tweener curTween;
    private AttackData currentAttackData;

    private AttackData CurAttackData {
        get {
            if (IngameData.currentGameMode != GameMode.EventBoss)
                return attackData[CurrentPhaseIndex];
            else
                return bossModeAttackDatas[CurrentPhaseIndex];
        }
    }
    protected override BossAttack GetBossAttack() {
        return bossAttack;
    }

    public override void StartAttack() {
        currentAttackData = CurAttackData;
        ShowEffect();
    }
    private void ShowEffect() {
        bossAttack.B13Base.B13Hitbox.ActiveCollider(false);
        curTween = bossAttack.BossBase.BossMove.StartMoveFront(bossAttack.B13Base.B13Move.GetPointMoveB13(new Vector2(0.5f, 1f)), currentAttackData.SpeedBullet / 5, moveCuver2, null);
        curTween.OnUpdate(() => {
            if (curTween.ElapsedPercentage() > 0.9f) {
                bossIce.gameObject.SetActive(false);
                bossFire.gameObject.SetActive(false);
                Attack();
                curTween.Kill();
            }
        });
    }
    private void Attack() {

        Vector2 direction = (bossAttack.Target.position - transform.position).normalized;
        if (leftBullet) {
            Vector2 positionLeft = (Vector2)firePoint.position + Vector2.left * 0.5f;
            leftBullet.gameObject.SetActive(true);
            leftBullet.transform.position = positionLeft;
            leftBullet = ChangingBullet(leftBullet);
            leftBullet.Shoot(currentAttackData.SpeedBullet, direction, currentAttackData.Amplitude, currentAttackData.Cycle, false);
        }
        if (rightBullet) {
            Vector2 positionRight = (Vector2)firePoint.position + Vector2.right * 0.5f;
            rightBullet.gameObject.SetActive(true);
            rightBullet.transform.position = positionRight;
            rightBullet = ChangingBullet(rightBullet);
            rightBullet.Shoot(currentAttackData.SpeedBullet, direction, currentAttackData.Amplitude, currentAttackData.Cycle);
        }
        DOVirtual.DelayedCall(3f, () => {
            bossIce.gameObject.SetActive(true);
            bossFire.gameObject.SetActive(true);
            var ranVector2 = new Vector2(0.5f, 1.1f);
            bossAttack.transform.position = bossAttack.B13Base.B13Move.GetPointMoveB13(ranVector2);
            var posDefault = new Vector2(0.5f, 0.8f);
            bossAttack.transform.DOMove(bossAttack.B13Base.B13Move.GetPointMoveB13(posDefault), 1f);
            bossAttack.B13Base.B13Hitbox.ActiveCollider(true);
            DOVirtual.DelayedCall(1f, EndAttack);
        });
    }
    private void ResetState() {
        bossAttack.B13Base.B13Hitbox.ActiveCollider(true);
        leftBullet.gameObject.SetActive(false);
        rightBullet.gameObject.SetActive(false);
        ChangeBossAlpha(1);
        if (curTween != null) {
            curTween.Kill();
        }

    }
    public override void EndAttack() {
        ResetState();
        base.EndAttack();
    }
    public override void StopAttack() {
        ResetState();
        base.StopAttack();
    }
    private bool isMoving;
    public override void Updating() {
        if (isMoving) {
            if (bossAttack.B13Base.B13Move.HasOutBorder()) {
                bossAttack.B13Base.B13Hitbox.TurnOffShield();
                ChangeBossAlpha(1); //one time
                isMoving = false;
                bossAttack.B13Base.transform.DOKill(false);
                var ranVector2 = new Vector2(0.5f, 1.1f);
                bossAttack.transform.position = bossAttack.B13Base.B13Move.GetPointMoveB13(ranVector2);
                var posDefault = new Vector2(0.5f, 0.8f);
                bossAttack.transform.DOMove(bossAttack.B13Base.B13Move.GetPointMoveB13(posDefault), 2f);
                DOVirtual.DelayedCall(2f, EndAttack);
            }
        }
    }
    public override void Attacking() {
    }
    private void ChangeBossAlpha(float value) {
        bossIce.GetComponent<SpriteRenderer>().SetAlpha(value);
        bossFire.GetComponent<SpriteRenderer>().SetAlpha(value);
    }

    [System.Serializable]
    private class AttackData {
        [SerializeField] private float damagePercent;
        [SerializeField] private float duration;
        [SerializeField] private float deltaShot;
        [SerializeField] private float speedBullet;
        [SerializeField] private float amplitude;
        [SerializeField] private float cycle;

        public float DamagePercent {
            get => damagePercent;
        }
        public float DeltaShot {
            get => deltaShot;
        }
        public float Duration {
            get => duration;
        }
        public float SpeedBullet { get => speedBullet; }
        public float Amplitude { get => amplitude; }
        public float Cycle { get => cycle; }
    }
    public T ChangingBullet<T>(T bullet) where T : BulletBase {
        bullet.SetHitInfor(GetBossAttack().BossBase.BossStat.Atk.Value, null, GetBossAttack().BossBase);
        return bullet;
    }
}
