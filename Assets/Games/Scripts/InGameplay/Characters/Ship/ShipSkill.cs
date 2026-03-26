using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipSkill : CharacterSkill {
    private ShipBase shipBase;
    public ShipBase ShipBase {
        get {
            if (shipBase == null) {
                shipBase = CharacterBase as ShipBase;
            }
            return shipBase;
        }
    }

    private List<ModData> mods = new List<ModData>();
    private List<IModable> modInfos = new List<IModable>();
    private List<IChangeBulletModable> changeBulletMods = new List<IChangeBulletModable>();
    private List<IEffectAttackModable> effectAttackMods = new List<IEffectAttackModable>();
    private List<IKillMod> killMods = new List<IKillMod>();
    private List<ILevelupMod> levelupMods = new List<ILevelupMod>();
    private List<ShipSeflEffect> seflEffects = new List<ShipSeflEffect>();
    private FireballSatelliteModInfor fireballMod;
    private TurretModInfo turretModeInfo;
    private DroneShieldInfo droneShieldModeInfo;
    private EnergyShieldModInfo energyShieldModInfo;

    public List<IChangeBulletModable> ChangeBulletMods { get => changeBulletMods; }
    public List<IEffectAttackModable> EffectAttackMods { get => effectAttackMods; }
    public List<IKillMod> KillMods { get => killMods; }
    public List<ILevelupMod> LevelupMods { get => levelupMods; }
    public List<ShipSeflEffect> SeftEffects { get => seflEffects; }
    public List<ModData> Mods { get => mods; }
    public FireballSatelliteModInfor FireballMod { get => fireballMod; }

    public override void Initalize() {
        base.Initalize();
        mods.Clear();
        modInfos.Clear();
        changeBulletMods.Clear();
        effectAttackMods.Clear();
        killMods.Clear();
        seflEffects.Clear();
        levelupMods.Clear();
        fireballMod = null;
        turretModeInfo = null;
        droneShieldModeInfo = null;
        energyShieldModInfo = null;
    }

    public override void Destroy() {
        base.Destroy();
        mods.Clear();
        modInfos.Clear();
        changeBulletMods.Clear();
        effectAttackMods.Clear();
        killMods.Clear();
        seflEffects.Clear();
        levelupMods.Clear();
        fireballMod = null;
        turretModeInfo = null;
        droneShieldModeInfo = null;
        energyShieldModInfo = null;
    }

    public override void Updating() {
        base.Updating();
        for (int i = 0; i < seflEffects.Count; ++i) {
            seflEffects[i].Updating(Time.deltaTime);
        }
        if (turretModeInfo != null) {
            turretModeInfo.Updating();
        }
    }

    public void Revive() {
        CountdownEffects.Clear();
    }

    public void AddModData(ModData mod) {
        mods.Add(mod);
    }

    public bool HasMod(ModData mod) {
        return mods.Contains(mod);
    }

    public bool IsLimitApply(ModData mod, int limit = 2) {
        return GetCountMod(mod) >= limit;
    }
    public int GetCountMod(ModData mod) {
        var result = 0;
        foreach (var item in mods) {
            if (item.ModId == mod.ModId)
                result++;
        }
        return result;
    }
    public int GetCountMod(int modID) {
        var result = 0;
        foreach (var item in mods) {
            if (item.ModId == modID)
                result++;
        }
        return result;
    }
    public void AddChangeBulletMod(IChangeBulletModable mod) {
        changeBulletMods.Add(mod);
        modInfos.Add(mod);
    }

    public void AddEffectAttackMod(IEffectAttackModable mod) {
        effectAttackMods.Add(mod);
        modInfos.Add(mod);
    }

    public void AddKillAttackMod(IKillMod mod) {
        killMods.Add(mod);
        modInfos.Add(mod);
    }

    public void AddLevelupMod(ILevelupMod mod) {
        levelupMods.Add(mod);
        modInfos.Add(mod);
    }

    public void AddModInfo(IModable mod) {
        IModable modInfo = modInfos.Find(m => m.GetModInfor().GetId() == mod.GetModInfor().GetId());
        if (modInfo == null) {
            modInfos.Add(mod);
        }
        else {
            modInfo.GetModInfor().Upgrade();
        }
    }

    public void AddSelfEffect(ShipSeflEffect effect) {
        if (effect != null) {
            if (seflEffects.Contains(effect)) {
            }
            else {
                seflEffects.Add(effect);
                effect.EffectTo();
            }
        }
    }

    public void RemoveSelfEffect(ShipSeflEffect effect, bool removeAll = false) {
        if (effect != null) {
            if (removeAll) {
                int numberSkillInthis = seflEffects.Count(s => s.Equals(effect));
                for (int i = 0; i < numberSkillInthis; ++i) {
                    seflEffects.Remove(effect);
                }
            }
            else {
                seflEffects.Remove(effect);
            }
        }
    }

    public ModInfor GetModInfor(int id) {
        IModable modable = modInfos.FirstOrDefault(item => item.GetModInfor().GetId() == id);
        if (modable != null) {
            return modable.GetModInfor();
        }
        return null;
    }

    public T GetModInfor<T>(int id) where T : ModInfor {
        IModable mod = modInfos.FirstOrDefault(item => item.GetModInfor().GetId() == id && item.GetType() == typeof(T));
        if (mod != null) {
            return mod.GetModInfor() as T;
        }

        return null;
    }
    public T GetSelfEffect<T>(string id) where T : ShipSeflEffect {
        ShipSeflEffect effect = seflEffects.FirstOrDefault(e => e.Id.Equals(id));
        if (effect != null) {
            return effect as T;
        }
        return null;
    }

    public void AddTurretModInfo(TurretModInfo mod) {
        turretModeInfo = mod;
        modInfos.Add(mod);
    }
    public void AddDroneModInfo(DroneShieldInfo mod) {
        droneShieldModeInfo = mod;
    }
    public void AddEnergyShieldModInfo(EnergyShieldModInfo mod) {
        energyShieldModInfo = mod;
        modInfos.Add(mod);
    }
    public void AddFireBallModInfo(FireballSatelliteModInfor mod) {
        fireballMod = mod;
        modInfos.Add(mod);
    }

    public TurretModInfo GetTurretModInfo() {
        return turretModeInfo;
    }
    public DroneShieldInfo GetDroneModInfo() {
        return droneShieldModeInfo;
    }
}
