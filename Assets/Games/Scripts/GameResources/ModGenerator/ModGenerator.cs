using Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ModGenerator", menuName = "Mod/ModGenerator", order = 0)]
public class ModGenerator : ScriptableObject {
    [SerializeField] private ModData[] allMods;
    [SerializeField] private ModData[] patternMods;
    [SerializeField] private ModSlot[] slots;
    [SerializeField] private BulletUpModData specialModData;
    [SerializeField] private int[] specialModChances;
    [SerializeField] private ModData[] specials;

    private List<ModData> useableMods;
    private List<ModData> useablePatternMods;
    private List<ModData> useableSpecialMods;
    private ModData curentPatternMod;
    public ModData[] AllMods {
        get {
            return allMods;
        }
    }

    public ModData CurentPatternMod { get => curentPatternMod; }
    public void SetCurrentPatternMod(ModData mod) {
        curentPatternMod = mod;
    }

    public List<ModData> GetAllModUnlocked(int curLevelIndex) {
        List<ModData> unlockedMods = new List<ModData>();
        foreach (var m in allMods) {
            if (m.HasUnlocked(curLevelIndex)) {
                unlockedMods.Add(m);
            }
        }
        foreach (var m in patternMods) {
            if (m.HasUnlocked(curLevelIndex)) {
                unlockedMods.Add(m);
            }
        }
        return unlockedMods;
    }

    public ModData[] GetRandomModDatas(bool isGetPatternMod) {
        ModData[] randomMods;
        GetUseableMods();
        if (isGetPatternMod) {
            randomMods = RandomHelper.RandomInCollection(useablePatternMods.ToArray(), slots.Length);
        }
        else {
            randomMods = new ModData[slots.Length];
            for (int i = 0; i < slots.Length; ++i) {
                int loopTimes = 0;
                ModRarity randomRarity;
                ModData randomModData = null;
                do {
                    randomRarity = slots[i].GetRandomRarity();
                    randomModData = GetRandomModDataByRarity(randomRarity);
                    loopTimes++;
                    if (loopTimes > 10)
                        break;
                } while (IsMaxModSelected(randomMods, randomModData));
                randomMods[i] = randomModData;
            }
        }
        RandomHelper.Shuffle(randomMods);
        return randomMods;
    }
    private bool IsMaxModSelected(ModData[] mods, ModData randomModData) {
        int number = 0;
        foreach (var item in mods) {
            if (item == null)
                continue;
            if (item.ModId == randomModData.ModId)
                number++;
        }
        return number > 1;
    }

    public ModData[] GetRerollRandomModDatas(bool isGetPatternMod, ModData[] oldMods, bool isSpecial) {
        ModData[] randomMods = null;
        ModData[] olds = oldMods;
        GetUseableMods();
        if (isGetPatternMod) {
            int loop = 0;
            int indexRemove = UnityEngine.Random.Range(0, 3);
            do {
                loop++;
                if (loop > 10)
                    break;
                randomMods = RandomHelper.RandomInCollection(useablePatternMods.ToArray(), slots.Length);

            } while (randomMods.Contains(olds[indexRemove]));
        }
        else {
            randomMods = new ModData[slots.Length];
            int index = 0;
            if (isSpecial && SpecialModUsable()) {
                GetUseableSpecialMods();
                randomMods[index] = RandomHelper.RandomInCollection(useableSpecialMods);
                index = 1;
            }
            for (int i = index; i < slots.Length; ++i) {
                int loopTimes = 0;
                ModRarity randomRarity;
                ModData randomModData = null;
                do {
                    randomRarity = slots[i].GetRandomRarity();
                    randomModData = GetRandomModDataByRarity(randomRarity);
                    loopTimes++;
                    if (loopTimes > 10)
                        break;
                } while (IsMaxModSelected(randomMods, randomModData) || olds.Contains(randomModData) || (index != 0 && randomModData == randomMods[index]));
                randomMods[i] = randomModData;
            }
        }
        RandomHelper.Shuffle(randomMods);
        return randomMods;
    }
    private bool SpecialModUsable() {
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        foreach (var mod in specials) {
            if (ship.ShipSkill.Mods.Contains(mod))
                return false;
        }
        useableSpecialMods = new List<ModData>();
        for (int i = 0; i < specials.Length; i++) {
            if (useableMods.Contains(specials[i]))
                return true;
        }
        return false;
    }
    #region Tutorial
    public ModData[] GetModDatasInTrial(int level) {
        ModData[] randomMods = new ModData[slots.Length];
        GetUseableMods();
        if (level > 8) {
            for (int i = 0; i < slots.Length; ++i) {
                ModRarity randomRarity = slots[i].GetRandomRarity();
                ModData randomModData = GetRandomModDataByRarity(randomRarity);
                randomMods[i] = randomModData;
            }
        }
        else
        if (level == 2) {
            randomMods[0] = useablePatternMods[0];
            randomMods[1] = useablePatternMods[1];
            randomMods[2] = useablePatternMods[2];
        }
        else {
            int value = level == 3 || level == 5 || level == 8 ? 1 :
                level == 6 ? 4 : level == 7 ? 5 : 3;
            randomMods[0] = useableMods.FirstOrDefault(x => x.ModId == value);
            for (int i = 1; i < 3; i++) {
                ModRarity randomRarity = slots[i].GetRandomRarity();
                randomMods[i] = GetRandomModDataByRarity(randomRarity);
            }
        }
        if (randomMods[0] == null) {
            ModRarity randomRarity = slots[0].GetRandomRarity();
            randomMods[0] = GetRandomModDataByRarity(randomRarity);

        }
        RandomHelper.Shuffle(randomMods);
        return randomMods;
    }
    public ModData[] GetModDatasInTutorialIntroduce(int level) {
        ModData[] randomMods = new ModData[slots.Length];
        GetUseableMods();
        switch (level) {
            case 2:
                randomMods[0] = useablePatternMods[2];
                randomMods[1] = useablePatternMods[0];
                randomMods[2] = useablePatternMods[1];
                break;
            case 3:
                randomMods[0] = useableMods.FirstOrDefault(x => x.ModId == 2);
                randomMods[1] = useableMods.FirstOrDefault(x => x.ModId == 3);
                randomMods[2] = useableMods.FirstOrDefault(x => x.ModId == 16);
                break;
            case 4:
                randomMods[0] = useableMods.FirstOrDefault(x => x.ModId == 2);
                randomMods[1] = useableMods.FirstOrDefault(x => x.ModId == 1);
                randomMods[2] = useableMods.FirstOrDefault(x => x.ModId == 16);
                break;
            default:
                for (int i = 0; i < slots.Length; ++i) {
                    ModRarity randomRarity = slots[i].GetRandomRarity();
                    ModData randomModData = GetRandomModDataByRarity(randomRarity);
                    randomMods[i] = randomModData;
                }
                RandomHelper.Shuffle(randomMods);
                break;
        }
        return randomMods;
    }
    public ModData[] GetModDatasInTutorialPlayGame(int level) {
        ModData[] randomMods = new ModData[slots.Length];
        GetUseableMods();
        switch (level) {
            case 2:
                randomMods[0] = useablePatternMods[0];
                randomMods[1] = useablePatternMods[1];
                randomMods[2] = useablePatternMods[2];
                break;
            case 3:
                randomMods[1] = useableMods.FirstOrDefault(x => x.ModId == 1);
                do {
                    ModRarity randomRarity0 = slots[0].GetRandomRarity();
                    randomMods[0] = GetRandomModDataByRarity(randomRarity0);
                } while (randomMods[0].ModId == 1);
                do {
                    ModRarity randomRarity2 = slots[2].GetRandomRarity();
                    randomMods[2] = GetRandomModDataByRarity(randomRarity2);
                } while (randomMods[2].ModId == 1);
                break;
            case 8:
                randomMods[0] = useableMods.FirstOrDefault(x => x.ModId == 1);
                for (int i = 1; i < slots.Length; ++i) {
                    do {
                        ModRarity randomRarity = slots[i].GetRandomRarity();
                        ModData randomModData = GetRandomModDataByRarity(randomRarity);
                        randomMods[i] = randomModData;
                    } while (randomMods[i].ModId == 1);
                }
                RandomHelper.Shuffle(randomMods);
                break;
            case int n when (n > 3 && n <= 8):
                for (int i = 0; i < slots.Length; ++i) {
                    do {
                        ModRarity randomRarity = slots[i].GetRandomRarity();
                        ModData randomModData = GetRandomModDataByRarity(randomRarity);
                        randomMods[i] = randomModData;
                    } while (randomMods[i].ModId == 1);
                }
                RandomHelper.Shuffle(randomMods);
                break;
            default:
                for (int i = 0; i < slots.Length; ++i) {
                    ModRarity randomRarity = slots[i].GetRandomRarity();
                    ModData randomModData = GetRandomModDataByRarity(randomRarity);
                    randomMods[i] = randomModData;
                }
                RandomHelper.Shuffle(randomMods);
                break;
        }
        return randomMods;
    }
    #endregion

    #region Gen Mod on New Level
    public ModData[] GetRandomModDatasOnLevelUp(bool isGetPatternMod) {
        ModData[] randomMods;
        GetUseableMods();
        if (isGetPatternMod) {
            randomMods = RandomHelper.RandomInCollection(useablePatternMods.ToArray(), slots.Length);
        }
        else {
            randomMods = new ModData[slots.Length];
            var data = GameResources.Instance.LevelProgress.Datas.UnlockFeatures.GetUnlockMods(GameResources.Instance.LevelProgress.GetCurrentLevel() + 1);
            var index = 0;
            if (data != null) {
                foreach (var item in data) {
                    if (index < randomMods.Length && useableMods.Contains(item)) {
                        randomMods[index] = item;
                        index++;
                    }
                }
                if (index == randomMods.Length) {
                    return randomMods;
                }
            }
            for (int i = index; i < randomMods.Length; i++) {
                int loopTimes = 0;
                ModRarity randomRarity;
                ModData randomModData = null;
                do {
                    randomRarity = slots[i].GetRandomRarity();
                    randomModData = GetRandomModDataByRarity(randomRarity);
                    loopTimes++;
                    if (loopTimes > 10)
                        break;
                } while (IsMaxModSelected(randomMods, randomModData));
                randomMods[i] = randomModData;
            }
        }
        RandomHelper.Shuffle(randomMods);
        return randomMods;
    }
    #endregion

    #region Random Wave 4ngel
    public ModData GetRandomModDatasWithIndexs(int[] indexs) {
        GetUseableMods();
        var indexSelect = UnityEngine.Random.Range(0, indexs.Length);
        ModData randomMods = useableMods.FirstOrDefault(x => x.ModId == indexs[indexSelect]);
        return randomMods;
    }

    public (ModData[], int[]) GetRandomModDatasMultiple(bool isGetPatternMod) {
        ModData[] randomMods;
        int[] indexMods = new int[3];
        GetUseableMods();
        if (isGetPatternMod) {
            randomMods = RandomHelper.RandomInCollection(useablePatternMods.ToArray(), slots.Length);
            indexMods[0] = 0;
            indexMods[1] = 1;
            indexMods[2] = 2;
        }
        else {
            randomMods = new ModData[slots.Length];
            for (int i = 0; i < slots.Length; ++i) {
                ModRarity randomRarity = slots[i].GetRandomRarity();
                ModData randomModData = GetRandomModDataByRarity(randomRarity);
                randomMods[i] = randomModData;
                indexMods[i] = randomModData.ModId + 2;
            }
        }
        RandomHelper.Shuffle(randomMods, indexMods);
        return (randomMods, indexMods);
    }
    #endregion

    public Sprite GetRandomModIcon() {
        return useableMods[UnityEngine.Random.Range(0, useableMods.Count)].Icon;
    }

    private void GetUseableMods() {
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        useableMods = new List<ModData>();
        foreach (var mod in allMods) {
            if (mod.CanApplyTo(ship)) {
                useableMods.Add(mod);
            }
        }

        useablePatternMods = new List<ModData>();
        foreach (var patterPrefab in ship.ShipAttack.CurrentAttackComponent.CurrentPattern.GetCombiePatterns()) {
            PatternModData mod = patterPrefab.ModData;
            mod.SetShipPattern(patterPrefab.ShipPattern);
            if (mod.CanApplyTo(ship)) {
                useablePatternMods.Add(mod);
            }
        }
    }
    private void GetUseableSpecialMods() {
        ShipBase ship = GameManager.Instance.GameLoader.Ship;
        useableSpecialMods = new List<ModData>();
        foreach (var mod in specials) {
            if (mod.CanApplyTo(ship)) {
                useableSpecialMods.Add(mod);
            }
        }
    }
    private ModData GetRandomModDataByRarity(ModRarity rarity) {
        List<ModData> mods = GetModsByRarity(rarity);
        return RandomHelper.RandomInCollection(mods);
    }

    private List<ModData> GetModsByRarity(ModRarity rarity) {
        List<ModData> mods = new List<ModData>();
        mods = CheckSpecialMod(mods, rarity);
        if (mods.Count != 0)
            return mods;

        do {
            foreach (var mod in useableMods) {
                if (mod.Rarity == rarity) {
                    mods.Add(mod);
                }
            }
            rarity--;
            if (rarity < 0)
                mods.Add(useableMods[0]);
        } while (mods.Count == 0);
        return mods;
    }
    private List<ModData> CheckSpecialMod(List<ModData> mods, ModRarity rarity) {
        if (rarity == ModRarity.High && useableMods.Contains(specialModData)) {
            ShipBase ship = GameManager.Instance.GameLoader.Ship;
            var specialChance = specialModChances[ship.ShipSkill.GetCountMod(specialModData)];
            if (RandomHelper.RandomWithPercent(specialChance))
                mods.Add(specialModData);
        }
        return mods;
    }
    [Serializable]
    public class ModSlot {
        [SerializeField] private ModRarity[] rarities;

        public ModRarity GetRandomRarity() {
            return RandomHelper.RandomInCollection(rarities);
        }
    }
}
