using UnityEngine;

public class DroneHitBox : CharacterHitbox {
    [Header("Drone Hit Box")]
    [SerializeField] private float hitInvulnerableDurantion = 0.5f;
    [SerializeField] private Transform shield;
    [SerializeField] private Transform energyShield;
    [SerializeField] private Transform reflectiveShield;
    private DroneBase droneBase;
    public DroneBase DroneBase {
        get {
            if (droneBase == null) {
                droneBase = CharacterBase as DroneBase;
            }
            return droneBase;
        }
    }

    protected override void TakeHitDamage(int damage, Vector2 position, ObjectBase causer, HitType type = HitType.Normal) {
        base.TakeHitDamage(damage, position, causer, type);
        TurnOnInvulnerable(2);
    }

    protected override void AddAssisCauser(ObjectBase assiser) {
    }

    protected override void RemoveAssisCauser(ObjectBase laster) {
    }

    public void TurnOnProtectShield() {
        DroneBase.ProtectShield.TurnOn();
        DroneBase.ProtectShield.SetTarget(DroneBase.transform);
        TurnOnInvulnerable(-1);
    }

    public void TurnOffProtectShield() {
        DroneBase.ProtectShield.TurnOff();
        TurnOffInvulnerable();
    }
    public void TurnOnShield(bool isProtectShield, int hp = 1000, float dodgeRate = 0, float timeReborn = 10f) {
        if (isProtectShield)
            TurnOnProtectShield();
        else
            DroneBase.EnableEnergyShield(hp, dodgeRate, timeReborn);
    }

    public void TurnOffShield(bool isProtectShield) {
        if (isProtectShield)
            TurnOffProtectShield();
        else
            DroneBase.DisableEnergyShield();
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag(GameTag.Enemy)) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                int damage = Mathf.CeilToInt(DroneBase.DroneStat.Atk.Value * DroneBase.DroneStat.ColliderDamage.Value);
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
        hitboxInfor.SetInfor(damage, null, DroneBase);
        return hitboxInfor;
    }
}
