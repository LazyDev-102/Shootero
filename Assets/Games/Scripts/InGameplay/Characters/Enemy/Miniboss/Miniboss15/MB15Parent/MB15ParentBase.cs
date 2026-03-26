using Helper;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MB15ParentStat), typeof(MB15ParentHealth), typeof(MB15ParentAttack))]
[RequireComponent(typeof(MB15ParentMove), typeof(MB15ParentHitbox), typeof(MB15ParentStat))]
[RequireComponent(typeof(MB15ParentSkill))]
public class MB15ParentBase : MinibossBase {
    #region References
    private MB15ParentHealth mb15ParentHealth;
    public MB15ParentHealth MB15ParentHealth {
        get {
            if (mb15ParentHealth == null) {
                mb15ParentHealth = EnemyHealth as MB15ParentHealth;
            }
            return mb15ParentHealth;
        }
    }

    private MB15ParentStat mb15ParentStat;
    public MB15ParentStat MB15ParentStat {
        get {
            if (mb15ParentStat == null) {
                mb15ParentStat = EnemyStat as MB15ParentStat;
            }
            return mb15ParentStat;
        }
    }

    private MB15ParentMove mb15ParentMove;
    public MB15ParentMove MB15ParentMove {
        get {
            if (mb15ParentMove == null) {
                mb15ParentMove = EnemyMove as MB15ParentMove;
            }
            return mb15ParentMove;
        }
    }
    private MB15ParentAttack mb15ParentAttack;
    public MB15ParentAttack MB15ParentAttack {
        get {
            if (mb15ParentAttack == null) {
                mb15ParentAttack = EnemyAttack as MB15ParentAttack;
            }
            return mb15ParentAttack;
        }
    }
    #endregion

    #region Attack
    [SerializeField] private MB15ChildBase eChild;
    [SerializeField] private readonly int count = 2;
    [SerializeField] private float miniumDistance;
    [SerializeField] private float damagePercent;
    [SerializeField] private float duration;
    [SerializeField] private Area leftArea;
    [SerializeField] private float rotateSpeed;

    private Vector2 spawnPosition;
    private List<MB15ChildBase> mb15ChildBases = new List<MB15ChildBase>();

    public override void Spawn() {
        base.Spawn();
        mb15ChildBases.Clear();
        for (int i = 0; i < count; ++i) {
            spawnPosition = new Vector2(Random.Range(0, 50), Random.Range(0, 50));
            var e = GameManager.Instance.GameLoader.SpawnEnemy(eChild, spawnPosition);
            e.SetParent(this);
            e.SetInfo(MB15ParentStat.MaxHP.Value / count, MB15ParentStat.Atk.Value);
            e.MB15ChildHealth.AddOnHpChanged(UpdateHealth);
            e.Initialize();
            e.MB15ChildAttack.SetShotDuration(duration);
            e.MB15ChildMove.SetTargetPosition(BorderHelper.GetWorldPointInsideArea(leftArea));
            e.MB15ChildMove.StartTargetPosition();
            e.MB15ChildMove.SetRotateSpeed(rotateSpeed);
            e.MB15ChildAttack.Fight();
            mb15ChildBases.Add(e);
        }
    }

    private void UpdateHealth(int hp, float ratio) {
        int totalHeal = 0;
        int curHeal = 0;
        foreach (var e in mb15ChildBases) {
            totalHeal += e.MB15ChildStat.MaxHP.Value;
            curHeal += e.MB15ChildHealth.CurrentHp;
        }
        MB15ParentHealth.DispatchOnHpChanged(curHeal, totalHeal);
    }

    public override void Die() {
        base.Die();
        foreach (var item in mb15ChildBases) {
            item.MB15ChildHealth.RemoveOnHpChanged(UpdateHealth);
        }
    }

    public override bool IsDie() {
        foreach (var e in mb15ChildBases) {
            if (!e.IsDie()) {
                return false;
            }
        }
        return true;
    }

    public bool CanMoveToPosition(Vector2 pos) {
        foreach (var e in mb15ChildBases) {
            if (Vector2.Distance(e.transform.position, pos) < miniumDistance) {
                return false;
            }
        }
        return true;

    }
    #endregion
}
