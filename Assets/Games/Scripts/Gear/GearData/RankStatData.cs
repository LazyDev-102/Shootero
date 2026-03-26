using UnityEngine;
using System.Linq;
using Helper;
using Gemmob;

namespace Gear_Data {
    [CreateAssetMenu(fileName = "RankStatData", menuName = "Resource/Gears/RankStatData")]
    public class RankStatData : ScriptableObject {
        [SerializeField] private RankStat[] rankStat;

        public RankStat[] RankStat { get => rankStat; }

        public StatModifier GetRankStatValue(int id, int rankIndex) {
            RankStat stat = RankStat.FirstOrDefault(s => s.StatData.Id == id);
            return stat.GetStatValue(rankIndex);
        }

        public void AddStat(int id, int rankIndex) {
            RankStat stat = RankStat.FirstOrDefault(s => s.StatData.Id == id);
            if (stat != null)
                stat.AddStat(rankIndex);
        }
        public void RemoveStat(int id, int rankIndex) {
            RankStat stat = RankStat.FirstOrDefault(s => s.StatData.Id == id);
            if (stat != null)
                stat.RemoveStat(rankIndex);
        }

        public int RandomRankStat() {
            return RandomHelper.RandomInCollection(rankStat).StatData.Id;
        }
        public RankStat GetRankStats(int id) {
            return RankStat.FirstOrDefault(x => x.StatData.Id == id);
        }
    }

    [System.Serializable]
    public class RankStat {
        [SerializeField] private Gear_Data.StatHardData statData;
        [SerializeField] private StatModifier[] statValues;

        public StatHardData StatData { get => statData; }
        public StatModifier[] Values { get => statValues; }

        public void AddStat(int rankIndex) {
            if (rankIndex < 0 || rankIndex >= statValues.Length) {
                Logs.LogError($"Out of range array: {StatData.name}");
                return;
            }
            StatData.AddStat(statValues[rankIndex]);
        }

        public void RemoveStat(int rankIndex) {
            if (rankIndex < 0 || rankIndex >= statValues.Length) {
                Logs.LogError($"Out of range array: {StatData.name}");
                return;
            }
            StatData.RemoveStat(statValues[rankIndex]);
        }

        public StatModifier GetStatValue(int rankIndex) {
            if (rankIndex < 0 || rankIndex >= statValues.Length) {
                Logs.LogError($"Out of range array: {StatData.name}");
                return null;
            }
            return statValues[rankIndex];
        }
    }
}
