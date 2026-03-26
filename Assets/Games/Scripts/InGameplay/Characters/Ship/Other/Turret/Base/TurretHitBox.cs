using UnityEngine;

public class TurretHitBox : CharacterHitbox {
    [Header("Turret Hit Box")]
    [SerializeField] private float hitInvulnerableDurantion = 0.5f;
    private TurretBase turretBase;
    public TurretBase TurretBase {
        get {
            if (turretBase == null) {
                turretBase = CharacterBase as TurretBase;
            }
            return turretBase;
        }
    }

    protected override void TakeHitDamage(int damage, Vector2 position, ObjectBase causer, HitType type = HitType.Normal) {
        base.TakeHitDamage(damage, position, causer, type);
        //TurnOnInvulnerable(hitInvulnerableDurantion);
    }

    protected override void AddAssisCauser(ObjectBase assiser) {
    }

    protected override void RemoveAssisCauser(ObjectBase laster) {
    }
    public virtual void TransferShield(bool state) {

    }
    public void TurnOnShield(bool isProtectShield, int hp = 1000, float dodgeRate = 0, float timeReborn = 10f) {
        if (isProtectShield) {
            TurretBase.ProtectShield.TurnOn();
            TurretBase.ProtectShield.SetTarget(TurretBase.transform);
            TurnOnInvulnerable(-1);
        }
        else {
            TurretBase.EnableEnergyShield(hp, dodgeRate, timeReborn);
            TurnOnInvulnerable(-1);
        }
    }

    public void TurnOffShield(bool isProtectShield) {
        if (isProtectShield) {
            TurretBase.ProtectShield.TurnOff();
            TurnOffInvulnerable();
        }
        //else {
        //    TurretBase.DisableEnergyShield();

        //}
    }
    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag(GameTag.Enemy)) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                int damage = Mathf.CeilToInt(TurretBase.TurretStat.Atk.Value * TurretBase.TurretStat.ColliderDamage.Value);
                takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
                if (takeHit is EnemyHitbox eHit) {
                    eHit.EnemyBase.EnemyMove.Knockback(transform.position);
                }
            }
        }
    }
    private HitInfor hitboxInfor;
    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, TurretBase);
        return hitboxInfor;
    }
}
