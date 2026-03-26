

using System.Collections;
using UnityEngine;
using Gemmob;

public class BossHitbox : EnemyHitbox {
    [SerializeField] private Shield shield;
    [SerializeField] protected Collider2D myCollider;
    [SerializeField] private float hitDamageCountdown = 0.5f;
    private bool isFreezeHitDame;
    private float freezeDuration = 0f;
    public void TurnOnShield() {
        if (shield) {
            shield.TurnOn();
        }
    }

    public void TurnOffShield() {
        if (shield) {
            shield.TurnOff();
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collider) {
        base.OnTriggerEnter2D(collider);
        SetDataBeforeFreeze(collider);
    }
    public override void Updating() {
        base.Updating();
        ReloadFreezeHitDamage();
    }
    private IEnumerator ReloadHitDamage() {
        myCollider.enabled = false;
        yield return Yielder.Wait(hitDamageCountdown);
        myCollider.enabled = true;
    }
    private void ReloadFreezeHitDamage() {
        if (isFreezeHitDame) {
            freezeDuration += Time.deltaTime;
            if (freezeDuration > hitDamageCountdown) {
                SetDataAfterFreeze();
            }
        }
    }
    protected virtual void SetDataBeforeFreeze(Collider2D collider) {
        if (collider.tag.Equals(GameTag.Player)) {
            myCollider.enabled = false;
            isFreezeHitDame = true;
        }
    }
    protected virtual void SetDataAfterFreeze() {
        myCollider.enabled = true;
        isFreezeHitDame = false;
        freezeDuration = 0;
    }
    protected override void CaculateTakeDamageWithCritical(HitInfor hit) {
        TakeHitDamage(Mathf.CeilToInt(hit.Damage.Value * hit.CritDamage), transform.position, hit.Causer, HitType.Crit);
    }
#if UNITY_EDITOR
    [SerializeField] BossHitbox reference;
    [UnityEngine.ContextMenu("Convert")]
    protected void Convert() {
        shield = reference.shield;
        myCollider = reference.myCollider;
        hitDamageCountdown = reference.hitDamageCountdown;
    }
#endif
}
