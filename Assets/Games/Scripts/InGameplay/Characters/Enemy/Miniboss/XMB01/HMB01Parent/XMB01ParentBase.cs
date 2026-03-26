using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MinibossStat), typeof(MinibossHealth), typeof(XMB01ParentAttack))]
[RequireComponent(typeof(MinibossMove), typeof(MinibossHitbox))]
[RequireComponent(typeof(MinibossSkill), typeof(XMB01ParentStateController))]
public class XMB01ParentBase : MinibossBase {
    #region References

    private XMB01ParentAttack mb09ParentAttack;
    public XMB01ParentAttack XMB01ParentAttack {
        get {
            if (mb09ParentAttack == null) {
                mb09ParentAttack = EnemyAttack as XMB01ParentAttack;
            }
            return mb09ParentAttack;
        }
    }
    #endregion

    #region Attack
    [SerializeField] private XMB01Base eChild;
    [SerializeField] private readonly int count = 2;
    [SerializeField] private float miniumDistance;
    private Vector2 spawnPosition;
    private List<XMB01Base> mb09Bases = new List<XMB01Base>();

    public override void PreloadIngame() {
        base.PreloadIngame();
        eChild.PreloadIngame();
        eChild.RegisterPool(count);
    }

    public override void Spawn() {
        base.Spawn();
        transform.localPosition = Vector3.zero;
        mb09Bases.Clear();
        for (int i = 0; i < count; ++i) {
            spawnPosition = new Vector2(Random.Range(0, 100), Random.Range(0, 100));
            var e = GameManager.Instance.GameLoader.SpawnEnemy(eChild, spawnPosition);
            e.MinibossStat.MaxHP.SetBaseValue(MinibossStat.MaxHP.Value / count);
            e.MinibossStat.Atk.SetBaseValue(MinibossStat.Atk.Value);
            e.MinibossHealth.AddOnHpChanged(UpdateHealth);
            e.Initialize();
            e.SetParent(this);
            mb09Bases.Add(e);
        }
    }

    private void UpdateHealth(int hp, float ratio) {
        int totalHeal = 0;
        int curHeal = 0;
        foreach (var e in mb09Bases) {
            totalHeal += e.MinibossStat.MaxHP.Value;
            curHeal += e.MinibossHealth.CurrentHp;
        }
        MinibossHealth.DispatchOnHpChanged(curHeal, totalHeal);
    }

    public override void Die() {
        base.Die();
        foreach (var item in mb09Bases) {
            item.MinibossHealth.RemoveOnHpChanged(UpdateHealth);
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
