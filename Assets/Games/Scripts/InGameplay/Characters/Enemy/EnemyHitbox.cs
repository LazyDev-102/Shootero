

using Helper;
using UnityEngine;

public class EnemyHitbox : CharacterHitbox {
    private EnemyBase enemyBase;
    public EnemyBase EnemyBase {
        get {
            if (enemyBase == null) {
                enemyBase = CharacterBase as EnemyBase;
            }
            return enemyBase;
        }
    }

    private HitInfor hitboxInfor;

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, EnemyBase);
        return hitboxInfor;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag(GameTag.Player) || collider.CompareTag(GameTag.ShieldShip)) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                int damage = (int)(EnemyBase.EnemyStat.Atk.Value * EnemyBase.EnemyStat.ColliderDamage.Value);
                takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
            }
        }
    }

    protected override void TakeHitDamage(int damage, Vector2 positionCollider, ObjectBase causer, HitType type = HitType.Normal) {
        base.TakeHitDamage(damage, positionCollider, causer, type);
        if (!EnemyBase.IsDie()) {
            EnemyBase.EnemyEffect.StartEnemyHitEffect();
        }
    }
    public override void TakeHitDamage(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (IsBlockTakeHit()) {
            return;
        }
        else if (hit == null) {
            return;
        }
        else if (RandomHelper.RandomWithProbability(hit.CritChance)) {
            CaculateTakeDamageWithCritical(hit);
        }
        else {
            TakeHitDamage(hit.Damage.Value, positionCollider, hit.Causer, type);
        }
        if (hit.Effects != null) {
            foreach (var effect in hit.Effects) {
                effect.EffectTo(CharacterBase, hit.Causer, hit.Damage, positionCollider);
            }
        }
    }
    protected virtual void CaculateTakeDamageWithCritical(HitInfor hit) {
        if (GameManager.Instance.GameLoader.Ship.ShipStat.CanSuperCritical() && EnemyBase as BossBase == null && EnemyBase as MinibossBase == null) {
            var ship = hit.Causer as ShipBase;
            if (ship != null) {
                TakeHitDamage(EnemyBase.EnemyHealth.CurrentHp, transform.position, hit.Causer, HitType.OneShot);
                return;
            }
        }
        TakeHitDamage(Mathf.CeilToInt(hit.Damage.Value * hit.CritDamage), transform.position, hit.Causer, HitType.Crit);
    }
}
