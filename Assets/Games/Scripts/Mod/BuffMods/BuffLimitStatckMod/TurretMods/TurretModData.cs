using Gemmob;
using Helper;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretModData", menuName = "Mod/Buff/Limited/Turret")]
public class TurretModData : BuffLimitStackModData {
    [SerializeField] private GameObject prefab;
    [SerializeField, Tooltip("Percent Damage of Ship")] private float percentDamage;
    [SerializeField, Tooltip("Percent HP of Ship")] private float percentHP;
    [SerializeField] private int maxTurretCount;
    [SerializeField] private float fireRate;
    [SerializeField] private float timeReborn;
    [SerializeField] private Area appearPos;
    [SerializeField] private ReflectiveShieldModData reflectiveShieldMod;

    public float Hp { get => percentDamage; }
    public float Damage { get => percentHP; }
    public GameObject Prefab { get => prefab; }
    public float FireRate { get => fireRate; }
    public float TimeReborn { get => timeReborn; }
    public int MaxTurretCount { get => maxTurretCount; }
    public Area AppearPos { get => appearPos; }
    public ReflectiveShieldModData ReflectiveShieldMod { get => reflectiveShieldMod; }

    public List<TurretBase> Turrets = new List<TurretBase>();
    public TurretBase TurretDefault;
    public ShipBase Character;
    public override void ApplyTo(ShipBase character) {
        base.ApplyTo(character);
        Character = character;
        Turrets.Clear();
        GameObject go = prefab.Spawn(GameManager.Instance.GameLoader.transform);
        TurretBase turret = go.GetComponent<TurretBase>();
        TurretInitData(turret);
        Turrets.Add(turret);
        character.ShipSkill.AddTurretModInfo(new TurretModInfo(this));
        SetTurretDefault(turret);
    }
    private void TurretInitData(TurretBase turret) {
        var shipStat = Character.ShipStat;
        if (percentDamage > 1)
            percentDamage = 1;
        if (percentHP > 1)
            percentHP = 1;
        turret.InitData((int)(shipStat.Atk.Value * percentDamage), (int)(shipStat.MaxHP.Value * percentHP), fireRate);
        turret.Initialize();
        turret.TurretMove.SetSpawnPos(BorderHelper.GetWorldPointInsideArea(appearPos));
    }
    private void SetTurretDefault(TurretBase turret) {
        TurretDefault = turret;
    }
}

public class TurretModInfo : ModInfor<TurretModData>, IModable {
    private IntStat hp;
    private IntStat damage;
    private FloatStat fireRate;
    private FloatStat timeReborn;
    private GameObject prefab;
    private List<Countdowner> countdowns;
    private List<TurretBase> turrets = new List<TurretBase>();
    public int maxTurretCount;
    public int currentTurretCount = 0;
    private Area appearPos;
    private ReflectiveShieldModData reflectiveShieldMod;

    private bool hasShield;
    private bool hasPattern;
    private ShipBase character;
    private TurretBase turretDefault;
    public TurretModInfo(TurretModData mod) : base(mod) {
        character = mod.Character;
        countdowns = new List<Countdowner>();
        turrets = mod.Turrets;
        hp = new IntStat((int)(mod.Damage * character.ShipStat.MaxHP.Value));
        damage = new IntStat((int)(mod.Hp * character.ShipStat.Atk.Value));
        prefab = mod.Prefab;
        fireRate = new FloatStat(mod.FireRate);
        timeReborn = new FloatStat(mod.TimeReborn);
        maxTurretCount = mod.MaxTurretCount;
        turretDefault = mod.TurretDefault;
        appearPos = mod.AppearPos;
        reflectiveShieldMod = mod.ReflectiveShieldMod;

        foreach (var item in turrets) {
            item.AddOnDie(CalculateSpawnTurret);
        }
    }

    public TurretModInfo(TurretModInfo mod) : base(mod) {

    }

    private void CalculateSpawnTurret() {
        Countdowner s = new Countdowner();
        s.StartCountdown(timeReborn.Value);
        countdowns.Add(s);
    }

    public void Updating() {
        if (countdowns.Count == 0)
            return;
        for (int c = 0; c < countdowns.Count; c++) {
            var newC = countdowns[c];
            if (newC.IsTimeOut()) {
                SpawnTurret();
                countdowns.Remove(countdowns[c]);
            }
            else {
                newC.Countdowning(Time.deltaTime);
                countdowns[c] = newC;
            }
        }
    }

    private void SpawnTurret() {
        if (currentTurretCount < maxTurretCount) {
            TurretBase turret = prefab.Spawn(GameManager.Instance.GameLoader.transform).GetComponent<TurretBase>();
            turret.InitData(damage.Value, hp.Value, fireRate.Value);
            turret.Initialize();
            turret.AddOnDie(CalculateSpawnTurret);
            SetPositionTurret(turret);
            turrets.Add(turret);
            ReloadDataChanged(character, turret);
        }

    }

    private void ReloadDataChanged(ShipBase character, TurretBase turret) {

        if (hasShield)
            ResetShield(turret);
        if (hasPattern)
            ChangePattern(character);
        foreach (var t in turrets) {
            t.TurretAttack.AddFireModifier(fireRate);
            //t.TurretStat.Atk.SetBaseValue(damage.Value, true);
            //t.TurretStat.MaxHP.SetBaseValue(hp.Value, true);
        }

        CheckCanReflectiveShield(character, turret);
    }

    private void CheckCanReflectiveShield(ShipBase character, TurretBase turret) {
        bool hasReflectiveShield = character.ShipSkill.HasMod(reflectiveShieldMod);
        if (hasReflectiveShield) {
            turret.EnableReflexShield(reflectiveShieldMod.PercentDamage);
        }
    }
    private void SetPositionTurret(TurretBase newTurret) {
        var result = false;
        var loopTime = 0;
        var posEnd = Vector2.zero;
        do {
            result = true;
            posEnd = BorderHelper.GetWorldPointInsideArea(appearPos);
            foreach (var item in turrets) {
                if (Vector2.Distance(item.transform.position, posEnd) < 3) {
                    result = false;
                    break;
                }
            }

            loopTime++;
            if (loopTime > 50) {
                result = true;
                break;
            }
        }
        while (result == false);

        newTurret.TurretMove.SetSpawnPos(posEnd);
    }
    public void ChangeHP(ShipBase ship, StatModifier value) {
        var maxHp = this.hp.Value;
        maxHp += (int)(value.Value * ship.ShipStat.MaxHP.Value);
        this.hp.SetBaseValue(maxHp);
        foreach (var t in turrets) {
            int preMaxHp = t.TurretStat.MaxHP.Value;
            t.TurretStat.MaxHP.SetBaseValue(hp.Value, true);
            int afterMaxHp = t.TurretStat.MaxHP.Value;
            t.TurretHealth.AddHp(afterMaxHp - preMaxHp);
            //TextShowupManager.Instance.ShowHealingText($"+ {afterMaxHp - preMaxHp}", t.TurretMove.MyRigi.position);
        }
    }
    public void ChangePattern(ShipBase ship) {
        hasPattern = true;
        foreach (var t in turrets) {
            t.TurretAttack.ChangePattern(ship);
        }
    }
    public void ChangeDamage(ShipBase ship, StatModifier value) {
        var maxDamage = this.damage.Value;
        maxDamage += (int)(value.Value * ship.ShipStat.Atk.Value);
        this.damage.SetBaseValue(maxDamage);
        foreach (var t in turrets) {
            t.TurretStat.Atk.SetBaseValue(damage.Value, true);
        }
    }
    public void ChangeFireRate(StatModifier value) {
        this.fireRate.SetBaseValue(fireRate.Value * (1 - value.Value));
        foreach (var t in turrets) {
            t.TurretAttack.AddFireModifier(fireRate);
        }
    }
    public void ChangeStack() {
        currentTurretCount++;
        SpawnTurret();
    }
    public void ChangeTimeReborn(StatModifier value) {
        timeReborn.AddModifier(value);
    }
    public void TransferShield(ShipBase character, FloatStat duration, FloatStat countdown, bool isProtectShield, int hp, float dodgeRate) {
        this.character = character;
        TurretShieldEffect turretShieldEffect = new TurretShieldEffect(character, turrets, duration.Value, countdown.Value, isProtectShield, hp, dodgeRate);
        character.ShipSkill.AddSelfEffect(turretShieldEffect);
        hasShield = true;
        foreach (var item in turrets) {
            CheckCanReflectiveShield(character, item);
        }
    }
    public void ResetShield(TurretBase turret) {
        TurretShieldEffect turretShieldEffect = character.ShipSkill.GetSelfEffect<TurretShieldEffect>(TurretShieldEffect.shiledId);
        turretShieldEffect.ResetTurret(turrets, turret);
    }
    public ModInfor GetModInfor() {
        return this;
    }

    public object Clone() {
        return new TurretModInfo(this);
    }
}

