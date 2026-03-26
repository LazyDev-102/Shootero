

using UnityEngine;

public abstract class ChestBase : ObjectBase {
    #region
    private ChestAttack chestAttack;
    public ChestAttack ChestAttack {
        get {
            if (chestAttack == null) {
                chestAttack = ObjectAttack as ChestAttack;
            }
            return chestAttack;
        }
    }

    private ChestMove chestMove;
    public ChestMove ChestMove {
        get {
            if (chestMove == null) {
                chestMove = ObjectMove as ChestMove;
            }
            return chestMove;
        }
    }

    private ChestStat chestStat;
    public ChestStat ChestStat {
        get {
            if (chestStat == null) {
                chestStat = ObjectStat as ChestStat;
            }
            return chestStat;
        }
    }

    private ChestHitbox chestHitbox;
    public ChestHitbox ChestHitbox {
        get {
            if (chestHitbox == null) {
                chestHitbox = ObjectHitbox as ChestHitbox;
            }
            return chestHitbox;
        }
    }
    private ChestHealth chestHealth;
    public ChestHealth ChestHealth {
        get {
            if (chestHealth == null) {
                chestHealth = GetComponent<ChestHealth>();
            }
            return chestHealth;
        }
    }
    private ChestEffect chestEffect;
    public ChestEffect ChestEffect {
        get {
            if (chestEffect == null) {
                chestEffect = GetComponent<ChestEffect>();
            }
            return chestEffect;
        }
    }
    #endregion

#if UNITY_EDITOR
    protected void Start() {
        if (GameManager.Instance.isTest) {
            Initialize();
        }
    }
#endif

    [SerializeField] protected AreaType spawnBorderType;
    [SerializeField] protected float spawnBorderOffset = 1;
    [SerializeField] protected CameraShakeType shakeType;
    [SerializeField] protected EnemyType eType;
    [SerializeField] protected ParticleSystem explosion;
    [SerializeField] protected int numberExplosion;
    [SerializeField] protected float deltaExplosion;
    [SerializeField] protected float radiusExplosion;
    [SerializeField] protected int numberChipFake;

    protected System.Action<ChestBase> onRemove;
    public virtual void Spawn() {
        Vector3 positionSpawn = Helper.BorderHelper.GetRandomPositionBorder(spawnBorderType, spawnBorderOffset);
        transform.position = positionSpawn;
    }

    public virtual void Despawn() {
        Destroy();
        GameManager.Instance.GameLoader.DespawnChest(this);
        onRemove?.Invoke(this);
    }

    public void AddOnRemove(System.Action<ChestBase> onRemove) {
        this.onRemove += onRemove;
    }

    public void RemoveOnRemove(System.Action<ChestBase> onRemove) {
        this.onRemove -= onRemove;
    }

    public void RemoveAllOnRemove() {
        onRemove = null;
    }

    public virtual void Die() {
        CameraShakeManager.Instance.ShakeCamera(shakeType);
        PlayEffect();
        SpawnChip();
        RemoveAllOnRemove();
        RemoveMe();
    }
    private void PlayEffect() {
        if (explosion) {
            GameManager.Instance.GameLoader.SpawnEffectExplosions(explosion, transform.position, numberExplosion, radiusExplosion, deltaExplosion);
        }
    }
    private void SpawnChip() {
        if (!GameManager.Instance.isTest) {
            GameResources.Instance.Drop.DropingChip(transform.position, eType, numberChipFake);
            SoundManager.Instance.PlayEnemyDestroy();
        }
    }
    protected virtual void RemoveMe() {
        GameManager.Instance.GameLoader.DespawnChest(this);
        onRemove?.Invoke(this);
        DispatchOnDie();
    }
    protected virtual void DispatchOnDie() {

    }
    public virtual bool IsDie() {
        return ChestHealth.CurrentHp <= 0;
    }
    public override void Initialize() {
        base.Initialize();
        ChestHealth.Initalize();
        ChestEffect?.Initialize();
    }
    public override void Destroy() {
        base.Destroy();
        ChestHealth.Destroy();
        ChestEffect?.Destroy();
    }
    public override void Updating() {
        base.Updating();
        ChestHealth.Updating();
        ChestEffect?.Updating();
    }
}
