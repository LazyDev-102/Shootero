

using Helper;
using UnityEngine;

public class ChestHitbox : CharacterHitbox {
    private ChestBase enemyBase;
    public ChestBase ChestBase {
        get {
            if (enemyBase == null) {
                enemyBase = ObjectBase as ChestBase;
            }
            return enemyBase;
        }
    }

    private HitInfor hitboxInfor;

    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, ChestBase);
        return hitboxInfor;
    }

    protected override void TakeHitDamage(int damage, Vector2 positionCollider, ObjectBase causer, HitType type = HitType.Normal) {
        ChestBase.ChestHealth.HPReduce(damage);
        onTakeHit?.Invoke(damage);
        TextShowupManager.Instance.ShowHitText(type, damage.ToString(), transform.position);
        if (!ChestBase.IsDie()) {
            ChestBase.ChestEffect.StartChestHitEffect();
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
        if (GameManager.Instance.GameLoader.Ship.ShipStat.CanSuperCritical()) {
            var ship = hit.Causer as ShipBase;
            if (ship != null) {
                TakeHitDamage(ChestBase.ChestHealth.CurrentHp, transform.position, hit.Causer, HitType.OneShot);
                return;
            }
        }
        TakeHitDamage(Mathf.CeilToInt(hit.Damage.Value * hit.CritDamage), transform.position, hit.Causer, HitType.Crit);
    }
}
