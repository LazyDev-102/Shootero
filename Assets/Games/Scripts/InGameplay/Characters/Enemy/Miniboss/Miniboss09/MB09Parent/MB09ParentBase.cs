using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB09ParentStat), typeof(MB09ParentHealth), typeof(MB09ParentAttack))]
[RequireComponent(typeof(MB09ParentMove), typeof(MB09ParentHitbox), typeof(MB09ParentStat))]
[RequireComponent(typeof(MB09ParentSkill))]
public class MB09ParentBase : MinibossBase {
    #region References
    private MB09ParentHealth mb09ParentHealth;
    public MB09ParentHealth MB09ParentHealth {
        get {
            if (mb09ParentHealth == null) {
                mb09ParentHealth = EnemyHealth as MB09ParentHealth;
            }
            return mb09ParentHealth;
        }
    }

    private MB09ParentStat mb09ParentStat;
    public MB09ParentStat MB09ParentStat {
        get {
            if (mb09ParentStat == null) {
                mb09ParentStat = EnemyStat as MB09ParentStat;
            }
            return mb09ParentStat;
        }
    }

    private MB09ParentMove mb09ParentMove;
    public MB09ParentMove MB09ParentMove {
        get {
            if (mb09ParentMove == null) {
                mb09ParentMove = EnemyMove as MB09ParentMove;
            }
            return mb09ParentMove;
        }
    }
    private MB09ParentAttack mb09ParentAttack;
    public MB09ParentAttack MB09ParentAttack {
        get {
            if (mb09ParentAttack == null) {
                mb09ParentAttack = EnemyAttack as MB09ParentAttack;
            }
            return mb09ParentAttack;
        }
    }
    #endregion

    #region Attack
    [SerializeField] private MB09Base eChild;
    [SerializeField] private readonly int count = 2;
    [SerializeField] private float miniumDistance;
    private Vector2 spawnPosition;
    private List<MB09Base> mb09Bases = new List<MB09Base>();

    public override void Spawn() {
        base.Spawn();
        mb09Bases.Clear();
        for (int i = 0; i < count; ++i) {
            spawnPosition = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            var e = GameManager.Instance.GameLoader.SpawnEnemy(eChild, spawnPosition);
            e.MB09Stat.MaxHP.SetBaseValue(MB09ParentStat.MaxHP.Value / count);
            e.MB09Health.AddOnHpChanged(UpdateHealth);
            e.Initialize();
            e.SetParent(this);
            mb09Bases.Add(e);
        }
    }

    private void UpdateHealth(int hp, float ratio) {
        int totalHeal = 0;
        int curHeal = 0;
        foreach (var e in mb09Bases) {
            totalHeal += e.MB09Stat.MaxHP.Value;
            curHeal += e.MB09Health.CurrentHp;
        }
        MB09ParentHealth.DispatchOnHpChanged(curHeal, totalHeal);
    }

    public override void Die() {
        base.Die();
        foreach (var item in mb09Bases) {
            item.MB09Health.RemoveOnHpChanged(UpdateHealth);
        }
    }

    public override bool IsDie() {
        foreach (var e in mb09Bases) {
            if (!e.IsDie()) {
                return false;
            }
        }
        return true;
    }

    public bool CanMoveToPosition(Vector2 pos) {
        foreach (var e in mb09Bases) {
            if (Vector2.Distance(e.transform.position, pos) < miniumDistance) {
                return false;
            }
        }
        return true;

    }
    #endregion
}
