using UnityEngine;


public abstract class ModData : ScriptableObject {
    [SerializeField] private int modId;
    [SerializeField] private ModType type;
    [SerializeField] private string nameMod;
    [SerializeField] private string modDescription;
    [SerializeField] private Sprite icon;
    [SerializeField] private ModRarity rarity;
    [SerializeField] private int maxStack = 1;
    [SerializeField] private int levelRequire = 0;
    [SerializeField] private ModData[] requireMods;
    [SerializeField] private ModData[] requireBetweenMods;
    [SerializeField] private ModData[] notContainMods;

    public int ModId { get => modId; }
    public ModType Type { get => type; set => type = value; }
    public string NameMod { get => nameMod; set => nameMod = value; }
    public Sprite Icon { get => icon; set => icon = value; }
    public ModRarity Rarity { get => rarity; set => rarity = value; }
    public int MaxStack { get => maxStack; set => maxStack = value; }
    public ModData[] RequireMods { get => requireMods; set => requireMods = value; }
    public ModData[] RequireBetweenMods { get => requireMods; }

    public ModData[] NotContainMods { get => notContainMods; }
    public string ModDescription { get => modDescription; }
    public int LevelRequire { get => levelRequire; }

    public bool HasUnlocked(int curLevelIndex) {
        return levelRequire <= curLevelIndex;
    }

    public virtual bool CanApplyTo(ShipBase character) {
        if (!FirstCondition(character))
            return false;
        if (GameResources.Instance.LevelProgress.GetCurrentLevel() < levelRequire)
            return false;
        foreach (ModData mod in requireMods) {
            if (!character.ShipSkill.HasMod(mod)) {
                return false;
            }
        }
        if (notContainMods != null && notContainMods.Length != 0) {
            foreach (ModData mod in notContainMods) {
                if (character.ShipSkill.HasMod(mod)) {
                    return false;
                }
            }
        }

        if (maxStack > 0) {
            ModInfor modInfor = character.ShipSkill.GetModInfor(modId);
            if (modInfor != null) {
                if (modInfor.CurrentStack >= maxStack) {
                    return false;
                }
            }
        }

        if (requireBetweenMods != null && requireBetweenMods.Length != 0) {
            foreach (ModData mod in requireBetweenMods) {
                if (character.ShipSkill.HasMod(mod)) {
                    return true;
                }
            }
            return false;
        }
        if (!EndCondition(character)) {
            return false;
        }
        return true;
    }
    public virtual void ApplyTo(ShipBase character) {
        character.ShipSkill.AddModData(this);
    }
    //public virtual void AddDuplicate(CharacterBase character, ModInfor skillInfor = null) {
    //    skillInfor.Upgrade();
    //    character.SkillerBase.AddModData(this);
    //}
    public virtual bool FirstCondition(ShipBase character) {
        return true;
    }
    public virtual bool EndCondition(ShipBase character) {
        return true;
    }
    public override bool Equals(object other) {
        if (other == null) {
            return false;
        }
        return this.modId == (other as ModData).modId;
    }

    public override int GetHashCode() {
        return this.modId.GetHashCode();
    }

    public virtual void PreloadOpenApp() {

    }
}

public abstract class ModInfor {
    protected int currentStack;
    public int CurrentStack { get => currentStack; }

    public ModInfor() {
        currentStack = 1;
    }

    public ModInfor(ModInfor mod) {
        currentStack = mod.currentStack;
    }

    public virtual void Upgrade() {
        currentStack++;
    }

    public virtual void Downgrade() {
        if (CurrentStack <= 1) {
            return;
        }
        currentStack--;
    }

    public abstract int GetId();
}

public interface IModable : System.ICloneable {
    ModInfor GetModInfor();
}

public abstract class ModInfor<T> : ModInfor where T : ModData {
    protected T modData;

    public ModInfor(T modData) {
        this.modData = modData;
    }

    public ModInfor(ModInfor<T> mod) : base(mod) {
        this.modData = mod.modData;
    }

    public override sealed int GetId() {
        return modData.ModId;
    }
}



public enum ModType {
    Offense, Deffense, Utility, Special
}

public enum ModRarity {
    Low = 1, Med = 2, High = 3
}

