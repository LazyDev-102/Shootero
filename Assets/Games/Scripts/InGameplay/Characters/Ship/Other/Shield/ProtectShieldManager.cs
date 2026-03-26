using Gemmob;
using UnityEngine;

public class ProtectShieldManager : Shield, IHitbox {
    [SerializeField] private ReflectiveShieldManager reflectiveShieldManager;
    [SerializeField] private ReflectiveShieldModData reflectiveShieldMod;
    private bool hasActive;
    private ShipSkill shipSkill;
    private Transform target;
    private void Awake() {
        shipSkill = GameManager.Instance.GameLoader.Ship.ShipSkill;
    }
    public void SetTarget(Transform target) {
        this.target = target;
    }
    //public void EnableReflexShield(float percentDamage, Transform target) {
    //    reflectiveShieldManager.EnableShield(true, percentDamage, target);
    //    hasActive = true;
    //}
    //public void DisableReflexShield() {
    //    hasActive = false;
    //    reflectiveShieldManager.gameObject.SetActive(false);
    //}

    public void TakeHit(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        //if (!hasActive)
        //    return;
        if (shipSkill.HasMod(reflectiveShieldMod))
            EventDispatcher.Instance.Dispatch(new EventKey.OnShieldHitDamage() { Causer = hit.Causer, Target = target/*, shieldType = ShieldType.ProtectShield*/ });
    }

    public Transform Transform() {
        return transform;
    }
}
