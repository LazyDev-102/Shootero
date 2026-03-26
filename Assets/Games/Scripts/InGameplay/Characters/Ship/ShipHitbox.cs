

using DG.Tweening;
using Gemmob;
using Helper;
using UnityEngine;

public class ShipHitbox : CharacterHitbox {
    [Header("ShipHitbox")]
    [SerializeField] private float hitInvulnerableDurantion = 0.5f;
    [SerializeField] private SatelliteManager satelliteManager;
    [SerializeField] private ReflectiveShieldManager reflectiveShieldManager;
    [SerializeField] private ProtectShieldManager protectShieldManager;
    [SerializeField] private EnergyShieldManager energyShieldManager;
    [SerializeField] private AuraSystemManager auraSystemManager;
    [SerializeField] private WallShieldManager wallShieldManager;
    private ShipBase shipBase;
    private HitInfor hitboxInfor;

    private bool invulnerableCheat;
    private Countdowner reviveCD;
    private bool isReviving;
    protected bool hitDamageInWave;
    private WallShieldManager wallShield;
    public bool HitDamageInWave { get => hitDamageInWave; }
    public EnergyShieldManager EnergyShield { get => energyShieldManager; }

    public ProtectShieldManager ProtectShieldManager { get => protectShieldManager; }
    public ReflectiveShieldManager ReflectiveShieldManager { get => reflectiveShieldManager; }
    public AuraSystemManager AuraSystemManager { get => auraSystemManager; }
    public WallShieldManager WallShieldManager { get => wallShield; }

    public bool InvulnerableCheat { get => invulnerableCheat; set => invulnerableCheat = value; }

    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = CharacterBase as ShipBase;
            }
            return shipBase;
        }
    }

    public override void Initialize() {
        base.Initialize();
        isReviving = false;
        reviveCD = new Countdowner();
        AssignEvent();
    }
    public override void Destroy() {
        base.Destroy();
        UnassignEvent();
    }
    private void AssignEvent() {
        EventDispatcher.Instance.AddListener<EventKey.GameStartWaveParam>(ResetHitDameInWave);
    }
    private void UnassignEvent() {
        EventDispatcher.Instance.RemoveListener<EventKey.GameStartWaveParam>(ResetHitDameInWave);
    }
    public override void Updating() {
        base.Updating();
        if (!isReviving)
            return;
        if (reviveCD.IsTimeOut()) {
            isReviving = false;
            SetLockTurnOffStatus(false);
        }
        else {
            reviveCD.Countdowning(Time.deltaTime);
        }
    }
    public void Revive() {
        TurnOnProtectShield(3);
        SetLockTurnOffStatus(true);
        reviveCD.StartCountdown(3);
        isReviving = true;
    }
    #region EventDispatcher
    private void ResetHitDameInWave(EventKey.GameStartWaveParam shipInfor) {
        hitDamageInWave = false;
    }
    #endregion
    #region TakeDame
    public override void TakeHitDamage(HitInfor hit, Vector2 positionCollider, HitType type = HitType.Normal) {
        if (GameManager.Instance.isTest) {
            return;
        }
        if (GameManager.Instance.GameLoader.Ship == null)
            return;
        if (IsBlockTakeHit()) {
            return;
        }
        if (RandomHelper.RandomWithProbability(ShipBase.ShipStat.Evasion.Value)) {
            Evasion(hit.Causer);
        }
        else {
            base.TakeHitDamage(hit, positionCollider, type);
        }
        hitDamageInWave = true;
        if (hit != null)
            EventDispatcher.Instance.Dispatch(new EventKey.OnShipHitDamage() { Causer = hit.Causer });
    }

    protected override void TakeHitDamage(int damage, Vector2 positionCollider, ObjectBase causer, HitType type = HitType.Normal) {
        if (invulnerableCheat || IsInvulnerable) {
            return;
        }
        if (GameManager.Instance.GameLoader.Ship == null)
            return;
        ShipStat shipStat = ShipBase.ShipStat;
        int damageAfterReduce = Mathf.CeilToInt((damage) * (1 - shipStat.DamageReduce.Value));
        if (RandomHelper.RandomWithPercent(shipStat.BlockProbibility.Value)) {
            int maxHp = shipStat.MaxHP.Value;
            if (1.0f * damageAfterReduce / maxHp < shipStat.BlockDamage.Value) {
                damageAfterReduce = 1;
            }
        }
        base.TakeHitDamage(damageAfterReduce, positionCollider, causer, type);
        TurnOnInvulnerable(hitInvulnerableDurantion, false);
        //ShipBase.ShipHealth.ResetHealHPByPercentLoop(); Old Mod regen 
    }
    protected override void AddAssisCauser(ObjectBase assiser) {
    }

    protected override void RemoveAssisCauser(ObjectBase laster) {
    }
    #endregion

    #region Satellite
    public void EnableAssaultSatellite(float percentDamage) {
        satelliteManager.EnableAssault(percentDamage);
    }
    public void EnableSatelliteQuantity() {
        satelliteManager.EnableSatelliteQuantity();
    }
    public void EnableAutomaticSatellite(float speed, float delayPerAttack, float percentDamage) {
        satelliteManager.EnableAutomaticSatellite(speed, delayPerAttack, percentDamage);
    }
    public void EnableFireBallSatellite() {
        satelliteManager.EnableFireBallSatellite();
    }
    public void EnableSatelliteRange(float[] range) {
        satelliteManager.EnableSatelliteRange(range);
    }
    public void DisableAssaultSatellite() {
        satelliteManager.gameObject.SetActive(false);
    }
    public void ChangeSatelliteRotationSpeed(float percent) {
        satelliteManager.ChangeRotateSpeed(percent);
    }
    public void ChangeSatelliteDelayAttackTime(float percent) {
        satelliteManager.ChangeAutomaticDeltaAttack(percent);
    }
    #endregion

    #region Shield

    public void TurnOnProtectShield(float time = -1) {
        protectShieldManager.SetTarget(ShipBase.transform);
        protectShieldManager.TurnOn();
        onInvulnerableEffect?.Invoke(time);
        TurnOnInvulnerable(time);
        if (time != -1) {
            DOVirtual.DelayedCall(time, TurnOffProtectShield);
        }
    }
    public void TurnOffProtectShield() {
        protectShieldManager.TurnOff();
        stopInvulnerableEffect?.Invoke();
        TurnOffInvulnerable();
    }
    public void TurnOnProtectShieldTutorial() {
        protectShieldManager.SetTarget(ShipBase.transform);
        protectShieldManager.TurnOn();
        onInvulnerableEffect?.Invoke(-1);
        TurnOnInvulnerable(-1);
    }

    public void TurnOffProtectShieldTutorial() {
        protectShieldManager.TurnOff();
        stopInvulnerableEffect?.Invoke();
        TurnOffInvulnerable();
    }
    public void TurnOnShield(bool isProtectShield, int hp = 1000, float dodgeRate = 0, float timeReborn = 10f) {
        if (isProtectShield)
            TurnOnProtectShield();
        else
            EnableEnergyShield(hp, dodgeRate, timeReborn);
    }
    public void TurnOffShield(bool isProtectShield) {
        if (isProtectShield)
            TurnOffProtectShield();
        else {
            DisableEnergyShield();
            energyShieldManager.TurnOff();
        }
    }
    public void TurnOnReflectiveShield() {
        protectShieldManager.TurnOff();
        energyShieldManager.gameObject.SetActive(false);
        reflectiveShieldManager.gameObject.SetActive(true);
    }

    public void EnableReflexShield(float percentDamage) {
        reflectiveShieldManager.EnableShield(true, percentDamage, ShipBase.transform);
        //protectShieldManager.EnableReflexShield(percentDamage, ShipBase.transform);
    }

    public void DisableReflectiveShield() {
        //protectShieldManager.DisableReflexShield();
        reflectiveShieldManager.DisableShield();
    }
    public void EnableProtectSatallite(float rotateSpeed, float distanceWithShip) {
        satelliteManager.gameObject.SetActive(true);
        satelliteManager.InitData(rotateSpeed, distanceWithShip);
        satelliteManager.Initialize();
    }
    public void EnableEnergyShield(int hp, float dodgeRate, float timeReborn) {
        energyShieldManager.EnableEnergyShield(hp, dodgeRate, timeReborn, DisableEnergyShield, () => EnergyShieldReborn(hp), ShipBase.transform);
        EnergyShieldReborn(hp);
    }
    private void EnergyShieldReborn(int hp) {
        onInvulnerableEffect?.Invoke(-1);
        ShipBase.ShipHealth.PlayerHPBar.TurnOnEnergyHpBar(hp);
        SetLockTurnOffStatus(true);
        TurnOnInvulnerable(-1);
    }
    public void DisableProtectSatallite() {
        satelliteManager.gameObject.SetActive(false);
    }
    public void DisableEnergyShield() {
        //energyShieldManager.gameObject.SetActive(false);
        stopInvulnerableEffect?.Invoke();
        SetLockTurnOffStatus(false);
        TurnOffInvulnerable();
        ShipBase.ShipHealth.PlayerHPBar.TurnOffEnergyHpBar();
    }
    public void SpawnWallShield(int hp, int damage, float timeReborn, float speed, float posSpawn) {
        var wallShield = wallShieldManager.Spawn(CommonHUD.Instance.transform, Vector3.right * 10);
        wallShield.transform.localPosition = Vector3.zero;
        wallShield.transform.position = Vector3.right * 10 + Vector3.up * (posSpawn - 0.5f) * 20;
        this.wallShield = wallShield;
        wallShield.Active(true)
                  .EnableWallShield(hp, damage, timeReborn, speed);
    }
    #endregion

    #region Trigger
    public HitInfor GetHitboxInfor(int damage) {
        if (hitboxInfor == null) {
            hitboxInfor = new HitInfor();
        }
        hitboxInfor.SetInfor(damage, null, ShipBase);
        return hitboxInfor;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag(GameTag.Enemy)) {
            IHitbox takeHit = collider.GetComponent<IHitbox>();
            if (takeHit != null) {
                int damage = Mathf.CeilToInt(ShipBase.ShipStat.Atk.Value * ShipBase.ShipStat.ColliderDamage.Value);
                takeHit.TakeHit(GetHitboxInfor(damage), transform.position);
                if (takeHit is EnemyHitbox eHit) {
                    eHit.EnemyBase.EnemyMove.Knockback(transform.position);
                }
            }
        }
    }

    protected override bool IsBlockTakeHit() {
        return base.IsBlockTakeHit() || ShipBase.IsDie();
    }
    #endregion
}
