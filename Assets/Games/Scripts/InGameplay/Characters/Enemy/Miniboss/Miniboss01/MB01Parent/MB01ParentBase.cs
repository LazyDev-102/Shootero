using Helper;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB01ParentStat), typeof(MB01ParentHealth), typeof(MB01ParentAttack))]
[RequireComponent(typeof(MB01ParentMove), typeof(MB01ParentHitbox), typeof(MB01ParentStat))]
[RequireComponent(typeof(MB01ParentSkill))]
public class MB01ParentBase : MinibossBase {
    #region References
    private MB01ParentHealth mb01ParentHealth;
    public MB01ParentHealth MB01ParentHealth {
        get {
            if (mb01ParentHealth == null) {
                mb01ParentHealth = EnemyHealth as MB01ParentHealth;
            }
            return mb01ParentHealth;
        }
    }

    private MB01ParentStat mb01ParentStat;
    public MB01ParentStat MB01ParentStat {
        get {
            if (mb01ParentStat == null) {
                mb01ParentStat = EnemyStat as MB01ParentStat;
            }
            return mb01ParentStat;
        }
    }

    private MB01ParentMove mb01ParentMove;
    public MB01ParentMove MB01ParentMove {
        get {
            if (mb01ParentMove == null) {
                mb01ParentMove = EnemyMove as MB01ParentMove;
            }
            return mb01ParentMove;
        }
    }
    private MB01ParentAttack mb01ParentAttack;
    public MB01ParentAttack MB01ParentAttack {
        get {
            if (mb01ParentAttack == null) {
                mb01ParentAttack = EnemyAttack as MB01ParentAttack;
            }
            return mb01ParentAttack;
        }
    }
    #endregion

    #region Attack
    [SerializeField] private MB01Base eChild;
    [SerializeField] private int count = 2;
    [SerializeField] private float miniumDistance;
    [SerializeField] private int childHP = 600;
    private Vector2 spawnPosition;
    private List<MB01Base> mb01Bases = new List<MB01Base>();
    public override void Spawn() {
        base.Spawn();
        mb01Bases?.Clear();
        for (int i = 0; i < count; ++i) {
            spawnPosition = new Vector2(Random.Range(-20, 20), Random.Range(0, 20));
            var e = GameManager.Instance.GameLoader.SpawnEnemy(eChild, spawnPosition);
            e.Initialize();
            e.MB01Stat.MaxHP.SetBaseValue(childHP);
            e.MB01Health.AddOnHpChanged(UpdateHealth);
            e.SetParent(this, AddChild, UpdateHealth);
            e.MB01Move.SetTargetPosition(BorderHelper.GetWorldPointInsideArea(new Area(Vector2.one * 0.2f, Vector2.one * 0.4f)));
            e.MB01Move.StartTargetPosition();
            AddChild(e);
        }
    }
    private void AddChild(MB01Base child) {
        mb01Bases.Add(child);
    }
    private void UpdateHealth(int hp, float ratio) {
        int totalHeal = 0;
        int curHeal = 0;
        foreach (var e in mb01Bases) {
            totalHeal += e.MB01Stat.MaxHP.Value;
            curHeal += e.MB01Health.CurrentHp;
        }
        MB01ParentHealth.DispatchOnHpChanged(curHeal, totalHeal);
    }

    public override void Die() {
        base.Die();
        foreach (var item in mb01Bases) {
            item?.MB01Health.RemoveOnHpChanged(UpdateHealth);
        }
    }

    public override bool IsDie() {
        foreach (var e in mb01Bases) {
            if (e != null && !e.IsDie()) {
                return false;
            }
        }
        return true;
    }

    public bool CanMoveToPosition(Vector2 pos) {
        foreach (var e in mb01Bases) {
            if (Vector2.Distance(e.transform.position, pos) < miniumDistance) {
                return false;
            }
        }
        return true;

    }
    #endregion
}
