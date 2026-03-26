

using Gemmob;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DroneAttack), typeof(DroneMove), typeof(DroneStat))]
[RequireComponent(typeof(DroneSkill), typeof(DroneLevel), typeof(DroneEffect))]
[RequireComponent(typeof(DroneStateController), typeof(DroneHealth), typeof(DroneHitBox))]

public class DroneBase : CharacterBase {
    #region References
    private DroneAttack droneAttack;
    public DroneAttack DroneAttack {
        get {
            if (droneAttack == null) {
                droneAttack = CharacterAttack as DroneAttack;
            }
            return droneAttack;
        }
    }

    private DroneMove droneMove;
    public DroneMove DroneMove {
        get {
            if (droneMove == null) {
                droneMove = CharacterMove as DroneMove;
            }
            return droneMove;
        }
    }

    private DroneStat droneStat;
    public DroneStat DroneStat {
        get {
            if (droneStat == null) {
                droneStat = CharacterStat as DroneStat;
            }
            return droneStat;
        }
    }

    private DroneSkill droneSkill;
    public DroneSkill DroneSkill {
        get {
            if (droneSkill == null) {
                droneSkill = CharacterSkill as DroneSkill;
            }
            return droneSkill;
        }
    }

    private DroneEffect droneEffect;
    public DroneEffect DroneEffect {
        get {
            if (droneEffect == null) {
                droneEffect = GetComponent<DroneEffect>();
            }
            return droneEffect;
        }
    }

    private DroneLevel droneLevel;
    public DroneLevel DroneLevel {
        get {
            if (droneLevel == null) {
                droneLevel = GetComponent<DroneLevel>();
            }
            return droneLevel;
        }
    }
    private DroneHealth droneHealth;
    public DroneHealth DroneHealth {
        get {
            if (droneHealth == null) {
                droneHealth = CharacterHealth as DroneHealth;
            }
            return droneHealth;
        }
    }

    private DroneHitBox droneHitbox;
    public DroneHitBox DroneHitbox {
        get {
            if (droneHitbox == null) {
                droneHitbox = CharacterHitbox as DroneHitBox;
            }
            return droneHitbox;
        }
    }

    #endregion
    private ShipBase shipBase;
    public Transform DroneTopTrans;
    public ShipBase ShipBase { get => shipBase; }
    [SerializeField] private float timeReborn = 30;
    [SerializeField] private SatelliteManager satelliteManager;
    [SerializeField] private ShipPreDieEffect shipPreDieEffect;
    [SerializeField] private ReflectiveShieldManager reflectiveShieldManager;
    [SerializeField] private ProtectShieldManager protectShieldManager;
    [SerializeField] private EnergyShieldManager energyShieldManager;


    private List<Countdowner> countdowns;
    public EnergyShieldManager EnergyShield { get => energyShieldManager; }
    public ProtectShieldManager ProtectShield { get => protectShieldManager; }

    public override void Initialize() {
        shipBase = GameManager.Instance.GameLoader.Ship;
        base.Initialize();
        DroneEffect.Initialize();
        countdowns = new List<Countdowner>();
    }

    public override void Updating() {
        base.Updating();
        DroneEffect.Updating();
        //CheckReborn();
    }

    public override void Destroy() {
        base.Destroy();
        DroneEffect.Destroy();
    }

    public override void Die() {
        SelfDestruction();
        DroneManager.Instance.SetStartCountdownReborn(this);
        if (DroneHealth.DroneHPBar) {
            DroneHealth.DroneHPBar.FadeToDisable();
        }
        base.Die();
        //Play Effect
        //gameObject.Recycle();
    }

    protected override void RemoveMe() {
        SelfDestruction();
    }
    public override void SelfDestruction() {
        DroneHealth.SelfDestroy();
        gameObject.SetActive(false);
        //this.Recycle();
    }
    public void Reborn() {
        gameObject.SetActive(true);
        DroneMove.PlayAppearEffect();
        DroneHealth.Initalize();
        DroneAttack.Reborn();
    }

    #region Satellite
    public void EnableAssaultSatellite(float percentDamage) {
        satelliteManager.EnableAssault(percentDamage);
    }
    public void DisableAssaultSatellite() {
        satelliteManager.gameObject.SetActive(false);
    }
    public void EnableAutomaticSatellite(float speed, float delayPerAttack, float percentDamage) {
        satelliteManager.EnableAutomaticSatellite(speed, delayPerAttack, percentDamage);
    }
    public void EnableFireBallSatellite() {
        satelliteManager.EnableFireBallSatellite();
    }
    public void EnableProtectSatallite(float rotateSpeed, float distanceWithShip) {
        satelliteManager.gameObject.SetActive(true);
        satelliteManager.InitData(rotateSpeed, distanceWithShip);
        satelliteManager.Initialize();
    }
    public void DisableProtectSatallite() {
        satelliteManager.gameObject.SetActive(false);
    }
    #endregion

    #region Shield
    public void EnableReflexShield(float percentDamage) {
        //protectShieldManager.EnableReflexShield(percentDamage, transform);
        reflectiveShieldManager.EnableShield(true, percentDamage, transform);
    }
    public void DisableReflectiveShield() {
        //protectShieldManager.DisableReflexShield();
        reflectiveShieldManager.DisableShield();
    }
    public void EnableEnergyShield(int hp, float dodgeRate, float timeReborn) {
        energyShieldManager.EnableEnergyShield(hp, dodgeRate, timeReborn, DisableEnergyShield, () => EnergyShieldReborn(hp), transform);
        EnergyShieldReborn(hp);
    }
    private void EnergyShieldReborn(int hp) {
        DroneHealth.DroneHPBar.TurnOnEnergyHpBar(hp);
        DroneHitbox.TurnOnInvulnerable(-1);
    }
    public void DisableEnergyShield() {
        //energyShieldManager.gameObject.SetActive(false);
        DroneHitbox.TurnOffInvulnerable();
        DroneHealth.DroneHPBar.TurnOffEnergyHpBar();
    }
    public void CalculateSpawnDrone() {
        Countdowner s = new Countdowner();
        s.StartCountdown(timeReborn);
        countdowns.Add(s);
    }
    private void CheckReborn() {
        if (countdowns.Count == 0)
            return;
        for (int c = 0; c < countdowns.Count; c++) {
            var newC = countdowns[c];
            if (newC.IsTimeOut()) {
                Reborn();
                countdowns.Remove(countdowns[c]);
            }
            else {
                newC.Countdowning(Time.deltaTime);
                countdowns[c] = newC;
            }
        }
    }
    #endregion
}
