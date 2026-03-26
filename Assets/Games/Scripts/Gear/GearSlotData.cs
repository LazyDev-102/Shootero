using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gear_Data {

    [CreateAssetMenu(fileName = "GearSlotData", menuName = "Resource/Gears/GearSlotData")]
    public class GearSlotData : ScriptableObject {
        [SerializeField] private int id;
        [SerializeField] private string gearSlotName;
        [SerializeField] private int currentLevel;
        [SerializeField] private GearType gearType;
        [SerializeField] private List<LevelGear> levels;
        [SerializeField] private List<LevelStatData> coreStats;
        [SerializeField] private GearSoftData item;
        [SerializeField] private int materialID;

        public bool IsMaxLevel { get => currentLevel == levels.Count - 1; }
        public string GearSlotName { get => gearSlotName; }
        public int CurrentLevel { get => currentLevel; }
        public GearType GearType { get => gearType; }
        public List<LevelGear> Levels { get => levels; }
        public List<LevelStatData> Stats { get => coreStats; }
        public GearSoftData ItemEquip { get => item; }
        public bool IsDroneSlot { get => gearType == GearType.Drone1 || gearType == GearType.Drone2; }
        public bool IsEquiped { get => item != null && item.IsEquiped && item.GearHardData != null; }
        public bool IsExist { get => item != default && item.GearHardData != null; }
        public int MaterialID { get => materialID; }
        #region SetData
        private void EquipBaseCoreStatsData() {
            coreStats[0].Equip(currentLevel);
            if (IsDroneSlot)
                coreStats[1].Equip(currentLevel);
        }
        public void SetData(int level, GearSoftData itemEquip) {
            currentLevel = level;
            item = itemEquip;
            EquipBaseCoreStatsData();
        }
        public bool Enhanceable() {
            if (currentLevel >= levels.Count - 1)
                return false;
            return levels[currentLevel].Enhanceable();
        }
        public bool EnoughLevel() {
            return currentLevel <= GameResources.Instance.LevelProgress.GetCurrentLevel();
        }
        #endregion

        #region Equip/UnEquip, Add/Remove Stat, Levelup
        public void EquipItem(GearSoftData gear) {
            item = gear;
            if (!IsDroneSlot)
                item.AddAllStat();
            item.SetIsEquiped(true);
            item.SetGearTypeSoft(gearType);
        }
        public void UnEquipItem() {
            if(!IsExist) {
                RemoveUnnecessary();
                return;
            }
            if(!IsDroneSlot)
                item.RemoveAllStat();
            item.SetIsEquiped(false);
            item = default;
            RemoveUnnecessary();
        }
        private void RemoveUnnecessary() {
            GameResources.Instance.GearInventory.UnEquipWithGearType(gearType);
        }
        public void RemoveItemStat() {
            if (!IsExist || IsDroneSlot)
                return;
            item.RemoveAllStat();
        }
        public void AddItemStat() {
            if (!IsExist || IsDroneSlot)
                return;
            item.AddAllStat();
        }
        public void Levelup() {
            if (IsMaxLevel)
                return;
            coreStats[0].Unequip(currentLevel);
            if (IsDroneSlot)
                coreStats[1].Unequip(0);
            currentLevel++;
            coreStats[0].Equip(currentLevel);
            if (IsDroneSlot)
                coreStats[1].Equip(0);
        }
        #endregion

        #region Editor
        private int chipEnhance = 200;
        private int matEnhanhce = 1;
        [SerializeField] ItemStack itemStack;
        [SerializeField] ItemStack itemMatStack;
        [ContextMenu("Load Price")]
        private void LoadPrice() {
            for (int i = 0; i < levels.Count; i++) {
                ItemStack newItem = new ItemStack(itemStack.Id, 0);
                levels[i].PriceUpgrade = newItem;
                if (levels[i].EnhanceRequire == null || levels[i].EnhanceRequire.Length == 0) {
                    ItemStack newItem1 = new ItemStack(itemMatStack.Id, 0);
                    levels[i].EnhanceRequire = new ItemStack[1] { newItem1 };
                }

                levels[i].PriceUpgrade.Amount = chipEnhance * (i + 1);
                levels[i].EnhanceRequire[0].Amount = matEnhanhce * (i + 1);
            }
        }
        [SerializeField] private int[] start;
        [SerializeField] private int[] offset;
        [SerializeField] private int[] amplitude;
        [ContextMenu("Load Stats")]
        private void LoadStats() {
            for (int i = 0; i < coreStats.Count; i++) {
                //StatModifier newModifier = coreStats[i].Values[0];
                coreStats[i].Values[0].Value = start[i];
                for (int j = 1; j < coreStats[i].Values.Length; j++) {
                    //coreStats[i].Values[j].Type = newModifier.Type;
                    coreStats[i].Values[j].Value = coreStats[i].Values[j - 1].Value + offset[i] + amplitude[i] * (j / 20);
                }
            }
        }
        [ContextMenu("Check Item")]
        public void CheckHasItem() {
            if (!GameResources.Instance.GearInventory.GearItems.Contains(item)) {
                UnEquipItem();
            }
        }
        #endregion
    }
}