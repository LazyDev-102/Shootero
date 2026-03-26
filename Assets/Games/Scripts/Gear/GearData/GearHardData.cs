using System;
using System.Collections.Generic;
using UnityEngine;
using Gemmob;


namespace Gear_Data {
    [CreateAssetMenu(fileName = "GearHardData", menuName = "Resource/Gears/GearHardData")]
    public class GearHardData : Item {
        [SerializeField] private GearType gearType;
        [SerializeField] private int order;
        [SerializeField] private Sprite iconSpecial;
        [SerializeField] private List<LevelGear> levels;
        [SerializeField] private List<LevelStatData> primaryStatDatas;
        [SerializeField] private StatHardData firstRankStatData;
        [SerializeField] private GameAction equipAction;
        [SerializeField] private GameAction unequipAction;

#if UNITY_EDITOR
        public string spreadSheetName;
        public string workSheetName;
#endif

        public GearType GearType { get => gearType; set => gearType = value; }
        public List<LevelGear> Levels { get => levels; }
        public int Order { get => order; set => order = value; }
        public List<LevelStatData> PrimaryStatDatas { get => primaryStatDatas; }
        public StatHardData FirstRankStatData { get => firstRankStatData; }

        public Sprite GetIcon(int currentRank) {
            if (gearType == GearType.Drone1 || gearType == GearType.Drone2)
                return Icon;
            return currentRank < 3 ? Icon : iconSpecial;
        }
        public RaretyData GetRarety(int rankIndex) {
            RaretyData[] raretyData = GameResources.Instance.GearData.GearRaretyData.RaretyData;
            if (rankIndex < 0 || rankIndex >= raretyData.Length) {
                Logs.LogError($"Out of range array: {name}");
                return raretyData[raretyData.Length - 1];
            }
            return raretyData[rankIndex];
        }

        public void Equip(int rankIndex, int levelIndex) {
            foreach (var stat in primaryStatDatas) {
                stat.Equip(levelIndex);
            }
            if (equipAction != null) {
                equipAction.Execute();
            }
        }

        public void Unequip(int rankIndex, int levelIndex) {
            foreach (var stat in primaryStatDatas) {
                stat.Unequip(rankIndex, levelIndex);
            }
            if (unequipAction != null) {
                unequipAction.Execute();
            }
        }

        public override void Claim(int amount) {
            GearInventory gearInventory = GameResources.Instance.GearInventory;
            for (int i = 0; i < amount; ++i) {
                gearInventory.Add(new GearSoftData(Id, 0));
            }
        }

        public virtual GearSoftData AddNewGear(int rank = 0) {
            GearSoftData newGear = new GearSoftData(Id, rank);
            GameResources.Instance.GearInventory.Add(newGear);
            if (rank >= 3)
                GameResources.Instance.RateUs.SetClaimEpicItemStatus(true);
            return newGear;
        }

        [ContextMenu("Add Gear")]
        private void AddGear() {
            AddNewGear();
        }

    }


    [Serializable]
    public class LevelStatData {
        [SerializeField] private StatHardData statData;
        [SerializeField] private StatModifier[] values;

        public StatHardData StatData { get => statData; }
        public StatModifier[] Values { get => values; set => values = value; }

        public void Equip(int levelIndex) {
            statData.AddStat(values[levelIndex]);
        }
        public void Unequip(int levelIndex) {
            statData.RemoveStat(values[levelIndex]);
        }

        public void Equip(int rankIndex, int levelIndex) {
            GameResources.Instance.GearData.RankStatData.AddStat(statData.Id, rankIndex);
            statData.AddStat(values[levelIndex]);
        }

        public void Unequip(int rankIndex, int levelIndex) {
            GameResources.Instance.GearData.RankStatData.RemoveStat(statData.Id, rankIndex);
            statData.RemoveStat(values[levelIndex]);
        }

        public string GetDescription(int rankIndex, int levelIndex) {
            float value = 0;
            StatModifier rankValue = GameResources.Instance.GearData.RankStatData.GetRankStatValue(statData.Id, rankIndex);
            StatModifier levelValue = values[levelIndex];
            value = rankValue.Value + levelValue.Value;
            return statData.GetDescription(value);
        }
    }

    [Serializable]
    public class LevelGear {
        [SerializeField] private ItemClaim[] sellPrices;
        [SerializeField] private ItemStack[] enhanceRequire;
        [SerializeField] private ItemStack priceUpgrade;

        public ItemClaim[] SellPrices { get => sellPrices; set => sellPrices = value; }
        public ItemStack[] EnhanceRequire { get => enhanceRequire; set => enhanceRequire = value; }
        public ItemStack PriceUpgrade { get => priceUpgrade; set => priceUpgrade = value; }

        public bool Enhanceable() {
            bool enoughCurrency = GameResources.Instance.Inventory.GetItem(priceUpgrade.Id).Amount >= priceUpgrade.Amount;
            bool enoughMaterial = true;
            for (int i = 0; i < enhanceRequire.Length; i++) {
                if (GameResources.Instance.Inventory.GetItem(enhanceRequire[i].Id).Amount < enhanceRequire[i].Amount) {
                    enoughMaterial = false;
                    break;
                }
            }
            return enoughCurrency && enoughMaterial;
        }
    }

}
