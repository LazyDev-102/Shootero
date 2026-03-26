using Gemmob;
using UnityEngine;

public abstract class MinibossBase : EnemyBase {
    #region References
    private MinibossAttack minibossAttack;
    public MinibossAttack MinibossAttack {
        get {
            if (minibossAttack == null) {
                minibossAttack = EnemyAttack as MinibossAttack;
            }
            return minibossAttack;
        }
    }

    private MinibossMove minibossMove;
    public MinibossMove MinibossMove {
        get {
            if (minibossMove == null) {
                minibossMove = EnemyMove as MinibossMove;
            }
            return minibossMove;
        }
    }

    private MinibossHealth minibossHealth;
    public MinibossHealth MinibossHealth {
        get {
            if (minibossHealth == null) {
                minibossHealth = EnemyHealth as MinibossHealth;
            }
            return minibossHealth;
        }
    }

    private MinibossStat minibossStat;
    public MinibossStat MinibossStat {
        get {
            if (minibossStat == null) {
                minibossStat = EnemyStat as MinibossStat;
            }
            return minibossStat;
        }
    }

    private MinibossHitbox minibossHitbox;
    public MinibossHitbox MinibossHitbox {
        get {
            if (minibossHitbox == null) {
                minibossHitbox = EnemyHitbox as MinibossHitbox;
            }
            return minibossHitbox;
        }
    }

    private MinibossSkill minibossSkill;
    public MinibossSkill MinibossSkill {
        get {
            if (minibossSkill == null) {
                minibossSkill = EnemySkill as MinibossSkill;
            }
            return minibossSkill;
        }
    }

    private MinibossEffect minibossEffect;
    public MinibossEffect MinibossEffect {
        get {
            if (minibossEffect == null) {
                minibossEffect = EnemyEffect as MinibossEffect;
            }
            return minibossEffect;
        }
    }

    #endregion
    [SerializeField] private int minibossIndex;
    [SerializeField] private float idleTimeAfterAppear;
    [SerializeField] private float idleTimeAfterAttack;
    [SerializeField] private bool isMainBoss = true;

    private Countdowner idleCountdowner;
    private bool isIdleAfterAppear;
    private bool isSpecialState;
    protected bool canDispatchMinibossSpawn = true;

    public int MinibossIndex {
        get {
            return minibossIndex;
        }
    }

    public bool IsSpecialState { get => isSpecialState; set => isSpecialState = value; }
    public override void Initialize() {
        base.Initialize();
        isIdleAfterAppear = false;
        if (canDispatchMinibossSpawn)
            EventDispatcher.Instance.Dispatch(new EventKey.OnMinibossSpawnParam() {
                minibossBase = this,
                isSpawn = true
            });
    }

    public override void Destroy() {
        base.Destroy();
        if (canDispatchMinibossSpawn)
            EventDispatcher.Instance.Dispatch(new EventKey.OnMinibossSpawnParam() {
                minibossBase = this,
                isSpawn = false
            });
    }

    public void StartIdleAfterAppear() {
        isIdleAfterAppear = true;
        idleCountdowner.StartCountdown(idleTimeAfterAppear);
        MinibossMove.StartMoveIdle();
    }

    public void StartIdleAfterAttack() {
        idleCountdowner.StartCountdown(idleTimeAfterAttack);
        MinibossMove.StartMoveIdle();
    }

    public void CountdownIdle() {
        idleCountdowner.Countdowning(Time.deltaTime);
    }

    public bool IsEndIdle() {
        return idleCountdowner.IsTimeOut();
    }

    public void EndIdle() {
        if (isIdleAfterAppear) {
            isIdleAfterAppear = false;
            GameManager.Instance.GameLoader.Ship.ShipAttack.ChangeStateShot(true);
        }
    }

    protected override void DispatchOnDie() {
        if (GameManager.Instance.GameMode == GameMode.EventHalloween) {
            if (isMainBoss)
                EventDispatcher.Instance.Dispatch(EventKey.HalloweenDefeatMiniBoss);
        }
        else
        if (GameManager.Instance.GameMode == GameMode.EventXmas) {
            if (isMainBoss)
                EventDispatcher.Instance.Dispatch(EventKey.XmasDefeatMiniBoss);
        }
    }
#if UNITY_EDITOR
    [SerializeField] MinibossBase reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        explosion = reference.explosion;
        numberExplosion = reference.numberExplosion;
        deltaExplosion = reference.deltaExplosion;
        radiusExplosion = reference.radiusExplosion;
        shakeType = reference.shakeType;
        type = reference.type;
        spawnBorderOffset = reference.spawnBorderOffset;
        spawnBorderType = reference.spawnBorderType;
        enableDropChip = reference.enableDropChip;
        score = reference.score;
        minibossIndex = reference.minibossIndex;
        idleTimeAfterAppear = reference.idleTimeAfterAppear;
        idleTimeAfterAttack = reference.idleTimeAfterAttack;
        isMainBoss = reference.isMainBoss;
    }
#endif
}
