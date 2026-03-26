using UnityEngine;
using Helper;
using DG.Tweening;
using Gemmob;

public class E07Attack : EnemyAttack {
    private E07Base e07Base;
    public E07Base E07Base {
        get {
            if (e07Base == null) {
                e07Base = EnemyBase as E07Base;
            }
            return e07Base;
        }
    }
    [SerializeField] private float aimTime;
    [SerializeField] private int numberAttack;
    [SerializeField] private float maxRadius;
    [SerializeField] private float radiateDuration;
    [SerializeField] private float delayAttackBoomTime;
    [SerializeField] private RangeFloatValue fadeRangeValue;
    [SerializeField] private Transform circleAttack;
    [SerializeField] private SpriteRenderer[] circleSprites;
    [SerializeField] private Explosioner boomPrefab;
    [SerializeField] private float boomRadius;
    [SerializeField] private float boomDamagePercent;

    private Countdowner aimCountdowner = new Countdowner();
    private Countdowner radiateCountdowner = new Countdowner();
    private Countdowner delayAttackBoomCD = new Countdowner();
    private Countdowner preAttackBoomCD = new Countdowner();
    private int numberRadiateCounter;
    private bool lockAttack;
    public override void Initialize() {
        base.Initialize();
        E07Base.RemoveAllOnDie();
        E07Base.AddOnDie(AttackBoom);
        lockAttack = true;
    }
    public void StartAimTarget() {
        aimCountdowner.StartCountdown(aimTime);
    }
    protected override void StartAttack() {
        base.StartAttack();
        delayAttackBoomCD.StartCountdown(delayAttackBoomTime);
        preAttackBoomCD.StartCountdown(0);
        lockAttack = false;

    }
    protected override void Attacking() {
        E07Base.E07Move.SetTargetMoveAttack((Vector2)Target.position);
        //StartRadiateCircle();
    }

    public override void EndAttack() {
        base.EndAttack();
        //EndRadiateCircle();
    }
    public override void Destroy() {
        EndAttack();
        base.Destroy();
        E07Base.RemoveAllOnDie();
    }
    public override bool CanAttack() {
        return aimCountdowner.IsTimeOut() && !isAttacking;
    }

    public void AimTarget() {
        E07Base.LookTarget();
        aimCountdowner.Countdowning(Time.deltaTime);
    }

    private void StartRadiateCircle() {
        radiateCountdowner.StartCountdown(radiateDuration);
        numberRadiateCounter = 0;
        ActiveCircle(true);
        circleAttack.Scale(0);
    }

    public void RadiatingCircle() {
        if (isAttacking) {
            if (numberRadiateCounter < numberAttack) {
                float ratio = 1 - radiateCountdowner.Countdown / radiateDuration;
                circleAttack.Scale(ratio * maxRadius);
                foreach (var sprite in circleSprites) {
                    sprite.ChangeAlpha(fadeRangeValue.GetRatioValue(ratio));
                }
                radiateCountdowner.Countdowning(Time.deltaTime);
                if (radiateCountdowner.IsTimeOut()) {
                    numberRadiateCounter++;
                    if (numberRadiateCounter < numberAttack) {
                        radiateCountdowner.StartCountdown(radiateDuration);
                    }
                    else {
                        EndRadiateCircle();
                    }
                }
            }
        }
    }

    private void EndRadiateCircle() {
        ActiveCircle(false);
    }

    private void ActiveCircle(bool active) {
        circleAttack.gameObject.SetActive(active);
    }

    public bool IsEndAttackCircle() {
        return radiateCountdowner.IsTimeOut() && numberRadiateCounter >= numberAttack;
    }
    public override void Updating() {
        if (!lockAttack && E07Base.E07Move.CompleteMoveToTarget()) {
            if (delayAttackBoomCD.IsTimeOut()) {
                lockAttack = true;
                AttackBoom();
                E07Suicide();
                delayAttackBoomCD.StartCountdown(delayAttackBoomTime);
            }
            else {
                delayAttackBoomCD.Countdowning(Time.deltaTime);
                if (preAttackBoomCD.IsTimeOut()) {
                    E07Base.E07Move.StartMoveFront(transform.position + transform.up * 2, 1, new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1)), null);
                    PreAttackBoom();
                    preAttackBoomCD.StartCountdown(100);
                }
                else {
                    preAttackBoomCD.Countdowning(Time.deltaTime);
                }
            }
        }
    }
    private void PreAttackBoom() {
        E07Base.E07Effect.StartBurningEffect(0, true);
    }
    private void AttackBoom() {
        var boomClone = GameManager.Instance.GameLoader.SpawnExplosion(boomPrefab, transform.position);
        if (boomClone) {
            boomClone.SetHitInfor((int)boomDamagePercent * E07Base.E07Stat.Atk.Value, null, E07Base)
                    .SetRadius(boomRadius)
                    //.SetRadiusEffect(boomRadius)
                    .Explosioning();
        }
    }

    private void E07Suicide() {
        EndAttack();
        //E07Base.Destroy();
        //E07Base.Recycle();
        GameManager.Instance.GameLoader.DespawnEnemy(E07Base);
    }
}
