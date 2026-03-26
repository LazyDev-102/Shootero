using Gemmob;
using UnityEngine;

[RequireComponent(typeof(TurretAttack), typeof(TurretMove), typeof(TurretStat))]
[RequireComponent(typeof(TurretSkill), typeof(TurretEffect))]
[RequireComponent(typeof(TurretStateController), typeof(TurretHealth), typeof(TurretHitBox))]

public class TurretBase : CharacterBase {
    #region References
    private TurretAttack turretAttack;
    public TurretAttack TurretAttack {
        get {
            if (turretAttack == null) {
                turretAttack = CharacterAttack as TurretAttack;
            }
            return turretAttack;
        }
    }

    private TurretMove turretMove;
    public TurretMove TurretMove {
        get {
            if (turretMove == null) {
                turretMove = CharacterMove as TurretMove;
            }
            return turretMove;
        }
    }

    private TurretStat turretStat;
    public TurretStat TurretStat {
        get {
            if (turretStat == null) {
                turretStat = CharacterStat as TurretStat;
            }
            return turretStat;
        }
    }

    private TurretSkill turretSkill;
    public TurretSkill TurretSkill {
        get {
            if (turretSkill == null) {
                turretSkill = CharacterSkill as TurretSkill;
            }
            return turretSkill;
        }
    }

    private TurretEffect turretEffect;
    public TurretEffect TurretEffect {
        get {
            if (turretEffect == null) {
                turretEffect = GetComponent<TurretEffect>();
            }
            return turretEffect;
        }
    }
    private TurretHealth turretHealth;
    public TurretHealth TurretHealth {
        get {
            if (turretHealth == null) {
                turretHealth = CharacterHealth as TurretHealth;
            }
            return turretHealth;
        }
    }

    private TurretHitBox turretHitbox;
    public TurretHitBox TurretHitbox {
        get {
            if (turretHitbox == null) {
                turretHitbox = CharacterHitbox as TurretHitBox;
            }
            return turretHitbox;
        }
    }

    #endregion
    private ShipBase shipBase;
    public ShipBase ShipBase { get => shipBase; }
    //private void OnEnable() {
    //    Initialize();
    //}
    public void InitData(int damage = 10, int hp = 10000, float fireRate = 1) {
        this.TurretStat.AddModifier(damage, hp, fireRate);
        //this.TurretMove.SetFocus();
    }
    public override void Initialize() {
        shipBase = GameManager.Instance.GameLoader.Ship;
        base.Initialize();
        TurretEffect.Initialize();
    }

    public override void Updating() {
        base.Updating();
        TurretEffect.Updating();
    }

    public override void Destroy() {
        base.Destroy();
        TurretEffect.Destroy();
    }
    public override void Die() {
        base.Die();
        gameObject.Recycle();
        TurretHealth.SelfDestroy();
        GameManager.Instance.GameLoader.SpawnEffectExplosion(explosion, transform.position);
        //gameObject.SetActive(false);
    }

    protected override void RemoveMe() {
    }

    public void ReBorn() {
        Initialize();
        gameObject.SetActive(true);
    }


    #region Shield

    [SerializeField] private ReflectiveShieldManager reflectiveShieldManager;
    [SerializeField] private ProtectShieldManager protectShieldManager;
    [SerializeField] private EnergyShieldManager energyShieldManager;

    public EnergyShieldManager EnergyShield { get => energyShieldManager; }
    public ProtectShieldManager ProtectShield { get => protectShieldManager; }
    public ReflectiveShieldManager ReflectiveShieldManager { get => reflectiveShieldManager; }

    public void EnableReflexShield(float percentDamage) {
        //protectShieldManager.EnableReflexShield(percentDamage, transform);
        reflectiveShieldManager.EnableShield(true, percentDamage, transform);
    }

    public void EnableEnergyShield(int hp, float dodgeRate, float timeReborn) {
        energyShieldManager.EnableEnergyShield(hp, dodgeRate, timeReborn, DisableEnergyShield, () => EnergyShieldReborn(hp), transform);
        EnergyShieldReborn(hp);
    }
    private void EnergyShieldReborn(int hp) {
        TurretHealth.TurretHPBar.TurnOnEnergyHpBar(hp);
        TurretHitbox.TurnOnInvulnerable(-1);
    }
    public void DisableEnergyShield() {
        //energyShieldManager.gameObject.SetActive(false);
        TurretHitbox.TurnOffInvulnerable();
        TurretHealth.TurretHPBar.TurnOffEnergyHpBar();
    }
    public void DisableReflectiveShield() {
        //protectShieldManager.DisableReflexShield();
        reflectiveShieldManager.DisableShield();
    }
    #endregion
}
