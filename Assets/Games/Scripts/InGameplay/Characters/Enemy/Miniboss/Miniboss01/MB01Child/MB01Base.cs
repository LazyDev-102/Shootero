using UnityEngine;

[RequireComponent(typeof(MB01Attack), typeof(MB01Move), typeof(MB01Health))]
[RequireComponent(typeof(MB01Stat), typeof(MB01Hitbox), typeof(MB01Skill))]
[RequireComponent(typeof(MB01Effect), typeof(MB01StateController))]
public class MB01Base : MinibossBase {
    #region References
    private MB01Attack mb01Attack;
    public MB01Attack MB01Attack {
        get {
            if (mb01Attack == null) {
                mb01Attack = EnemyAttack as MB01Attack;
            }
            return mb01Attack;
        }
    }

    private MB01Move mb01Move;
    public MB01Move MB01Move {
        get {
            if (mb01Move == null) {
                mb01Move = EnemyMove as MB01Move;
            }
            return mb01Move;
        }
    }

    private MB01Health mb01Health;
    public MB01Health MB01Health {
        get {
            if (mb01Health == null) {
                mb01Health = EnemyHealth as MB01Health;
            }
            return mb01Health;
        }
    }

    private MB01Stat mb01Stat;
    public MB01Stat MB01Stat {
        get {
            if (mb01Stat == null) {
                mb01Stat = EnemyStat as MB01Stat;
            }
            return mb01Stat;
        }
    }

    private MB01Hitbox mb01Hitbox;
    public MB01Hitbox MB01Hitbox {
        get {
            if (mb01Hitbox == null) {
                mb01Hitbox = EnemyHitbox as MB01Hitbox;
            }
            return mb01Hitbox;
        }
    }

    private MB01Skill mb01Skill;
    public MB01Skill MB01Skill {
        get {
            if (mb01Skill == null) {
                mb01Skill = EnemySkill as MB01Skill;
            }
            return mb01Skill;
        }
    }

    #endregion

    #region Special Attack
    [SerializeField] private MB01Base mbChild;
    [SerializeField] private int childCount;
    [SerializeField] private int childHP;
    [SerializeField] private int childDamage;

    private Vector2 spawnPosition;
    private MB01ParentBase myParent;
    private System.Action<MB01Base> onAddChild;
    private System.Action<int, float> onUpdateHealth;
    public MB01ParentBase MyParent { get => myParent; }

    public override void Initialize() {
        canDispatchMinibossSpawn = false;
        base.Initialize();
    }
    private bool hasSpawn;
    public override void Spawn() {
        if (hasSpawn)
            return;
        hasSpawn = true;
        base.Spawn();
    }
    public void SetParent(MB01ParentBase myParent, System.Action<MB01Base> onAddChild, System.Action<int, float> onUpdateHealth) {
        this.myParent = myParent;
        this.onAddChild = onAddChild;
        this.onUpdateHealth = onUpdateHealth;
    }
    public override void Die() {
        SpawnChild();
        base.Die();
    }
    private void SpawnChild() {
        if (childCount == 0 || mbChild == null)
            return;
        for (int i = 0; i < childCount; i++) {
            //spawnPosition = new Vector2(Random.Range(-10, 10), Random.Range(0, 20));
            MB01Base mbClone = GameManager.Instance.GameLoader.SpawnEnemy(mbChild, transform.position);
            mbClone.MB01Stat.MaxHP.SetBaseValue(childHP);
            mbClone.MB01Stat.Atk.SetBaseValue(childDamage);
            mbClone.MB01Health.AddOnHpChanged(onUpdateHealth);
            mbClone.Initialize();
            mbClone.SetParent(myParent, onAddChild, onUpdateHealth);
            onAddChild?.Invoke(mbClone);
        }
    }
    private void SetInfor(int hp, int damage) {
        MB01Stat.MaxHP.SetBaseValue(hp);
        MB01Stat.Atk.SetBaseValue(damage);
    }
    #endregion
}
