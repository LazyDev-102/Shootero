

using UnityEngine;

public abstract class TrapBase : ObjectBase {
    #region
    private TrapAttack trapAttack;
    public TrapAttack TrapAttack {
        get {
            if (trapAttack == null) {
                trapAttack = ObjectAttack as TrapAttack;
            }
            return trapAttack;
        }
    }

    private TrapMove trapMove;
    public TrapMove TrapMove {
        get {
            if (trapMove == null) {
                trapMove = ObjectMove as TrapMove;
            }
            return trapMove;
        }
    }

    private TrapStat trapStat;
    public TrapStat TrapStat {
        get {
            if (trapStat == null) {
                trapStat = ObjectStat as TrapStat;
            }
            return trapStat;
        }
    }

    private TrapHitbox trapHitbox;
    public TrapHitbox TrapHitbox {
        get {
            if (trapHitbox == null) {
                trapHitbox = ObjectHitbox as TrapHitbox;
            }
            return trapHitbox;
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
    [SerializeField] private EnemyType type;

    protected System.Action<TrapBase> onRemove;

    public EnemyType Type { get => type; }
    public virtual void Spawn() {
        Vector3 positionSpawn = Helper.BorderHelper.GetRandomPositionBorder(spawnBorderType, spawnBorderOffset);
        transform.position = positionSpawn;
    }

    public virtual void Despawn() {
        Destroy();
        GameManager.Instance.GameLoader.DespawnTrap(this);
        onRemove?.Invoke(this);
    }

    public virtual void ChangedStatWithMultipler(float multipler) {
        TrapStat.Atk.SetBaseValue((int)(TrapStat.AtkInit * multipler), true);
    }

    public virtual void ChangeStatTutorial(float multiScale = 1) {
        transform.localScale = Vector3.one * multiScale;
    }
    public void AddOnRemove(System.Action<TrapBase> onRemove) {
        this.onRemove += onRemove;
    }

    public void RemoveOnRemove(System.Action<TrapBase> onRemove) {
        this.onRemove -= onRemove;
    }

    public void RemoveAllOnRemove() {
        onRemove = null;
    }
}
