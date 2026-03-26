

using UnityEngine;

public abstract class EnemyBase : CharacterBase {
    #region References
    private EnemyAttack enemyAttack;
    public EnemyAttack EnemyAttack {
        get {
            if (enemyAttack == null) {
                enemyAttack = CharacterAttack as EnemyAttack;
            }
            return enemyAttack;
        }
    }

    private EnemyMove enemyMove;
    public EnemyMove EnemyMove {
        get {
            if (enemyMove == null) {
                enemyMove = CharacterMove as EnemyMove;
            }
            return enemyMove;
        }
    }

    private EnemyHealth enemyHealth;
    public EnemyHealth EnemyHealth {
        get {
            if (enemyHealth == null) {
                enemyHealth = CharacterHealth as EnemyHealth;
            }
            return enemyHealth;
        }
    }

    private EnemyStat enemyStat;
    public EnemyStat EnemyStat {
        get {
            if (enemyStat == null) {
                enemyStat = CharacterStat as EnemyStat;
            }
            return enemyStat;
        }
    }

    private EnemyHitbox enemyHitbox;
    public EnemyHitbox EnemyHitbox {
        get {
            if (enemyHitbox == null) {
                enemyHitbox = CharacterHitbox as EnemyHitbox;
            }
            return enemyHitbox;
        }
    }

    private EnemySkill enemySkill;
    public EnemySkill EnemySkill {
        get {
            if (enemySkill == null) {
                enemySkill = CharacterSkill as EnemySkill;
            }
            return enemySkill;
        }
    }

    private EnemyEffect enemyEffect;
    public EnemyEffect EnemyEffect {
        get {
            if (enemyEffect == null) {
                enemyEffect = CharacterEffect as EnemyEffect;
            }
            return enemyEffect;
        }
    }
    #endregion

    [SerializeField] protected EnemyType type;
    [SerializeField] protected AreaType spawnBorderType;
    [SerializeField] protected float spawnBorderOffset = 1;
    [SerializeField] protected bool enableDropChip = true;
    [SerializeField] protected int score = 100;

    protected bool canDropChip;
    protected System.Action<EnemyBase> onRemove;


    public EnemyType Type { get => type; }
    public int Score { get => score; }
    public bool EnableDropChip { get => enableDropChip; }
    public bool CanDropChip { get => canDropChip; set => canDropChip = value; }

    public void AddOnRemove(System.Action<EnemyBase> onRemove) {
        this.onRemove += onRemove;
    }

    public void RemoveOnRemove(System.Action<EnemyBase> onRemove) {
        this.onRemove -= onRemove;
    }

    public void RemoveAllOnRemove() {
        onRemove = null;
    }



    protected void Start() {
        if (GameManager.Instance.isTest) {
            Initialize();
        }
    }

    public override void Initialize() {
        base.Initialize();
        canDropChip = false;
    }

    public virtual void Spawn() {
        Vector3 positionSpawn = Helper.BorderHelper.GetRandomPositionBorder(spawnBorderType, spawnBorderOffset);
        transform.position = positionSpawn;
    }

    public override void Die() {
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
        if (enableDropChip && !GameManager.Instance.isTest) {
            GameResources.Instance.Drop.Droping(transform.position, this);
            if (canDropChip) {
                GameResources.Instance.Drop.DropingChip(transform.position, this);
                canDropChip = false;
            }
        }
        if (!GameManager.Instance.isTest) {
            if (type != EnemyType.Boss) {
                SoundManager.Instance.PlayEnemyDestroy();
            }
            else {
                SoundManager.Instance.PlayBossDestroy();
            }
            GameManager.Instance.GameController.AddGearDropPoint(transform.position, GetDropPoint());
        }
        //if(useShakeCamera) {
        //    CameraShakerManager.Instance.ShakeCamera(CameraShakeType.Weak);
        //}
        base.Die();
#if CHEAT
        Gemmob.EventDispatcher.Instance.Dispatch(new EventKey.OnEnemyDied());
#endif
    }
    protected virtual void DispatchOnDie() {
        if (GameManager.Instance.GameMode == GameMode.Conqueror) {
            ConquerorController controller = GameManager.Instance.GetGameController<ConquerorController>();
            if (controller.CurrentWaveInfo.CWaveType == WaveType.Normal) {
                GameResources.Instance.DailyMission.AddPointProgress(MissionType.DefeatEnemy, 1);
                Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnDefeatEnemy);
            }
        }
        if (GameManager.Instance.GameMode == GameMode.EventHalloween) {
            HalloweenModeController controller = GameManager.Instance.GetGameController<HalloweenModeController>();
            if (controller.CurrentWaveInfo.CWaveType == WaveType.Normal) {
                Gemmob.EventDispatcher.Instance.Dispatch(EventKey.HalloweenDefeatEnemy);
            }
        }
        if (GameManager.Instance.GameMode == GameMode.EventXmas) {
            XmasModeController controller = GameManager.Instance.GetGameController<XmasModeController>();
            if (controller.CurrentWaveInfo.CWaveType == WaveType.Normal) {
                Gemmob.EventDispatcher.Instance.Dispatch(EventKey.XmasDefeatEnemy);
            }
        }
    }
    protected override void RemoveMe() {
        GameManager.Instance.GameLoader.DespawnEnemy(this);
        onRemove?.Invoke(this);
        DispatchOnDie();
    }

    public virtual void ChangedStatWithMultipler(float multipler) {
        EnemyStat.Atk.SetBaseValue((int)(EnemyStat.AtkInit * multipler), true);
        EnemyStat.MaxHP.SetBaseValue((int)(EnemyStat.MaxHPInit * multipler), true);
    }
    public virtual void ChangedStatWithMultipler(int atkBase, int hpBase, float multipler) {
        EnemyStat.Atk.SetBaseValue((int)(atkBase * multipler), true);
        EnemyStat.MaxHP.SetBaseValue((int)(hpBase * multipler), true);
    }

    public virtual void ChangeStatWithEventValue(float atkPercent, float hpPercent, float size) {
        EnemyStat.Atk.SetBaseValue((int)(EnemyStat.Atk.GetBaseValue() * atkPercent));
        EnemyStat.MaxHP.SetBaseValue((int)(EnemyStat.MaxHP.GetBaseValue() * hpPercent));
        EnemyStat.Size.SetBaseValue(size);
        UpdateSize();
    }

    public void UpdateSize() {
        transform.localScale = Vector3.one * EnemyStat.Size.Value;
    }

    public void LookTarget() {
        if (EnemyAttack.Target) {
            EnemyMove.LookTarget(EnemyAttack.Target.position);
        }
    }
    private int GetDropPoint() {
        switch (type) {
            case EnemyType.Normal:
                return 1;
            case EnemyType.Elite:
                return 2;
            case EnemyType.Champion:
                return 3;
            case EnemyType.Boss:
                return 10;
            case EnemyType.Miniboss:
                return 5;
            default:
                return 0;
        }
    }
}



public enum EnemyType {
    Normal, Elite, Champion, Boss, Miniboss
}
