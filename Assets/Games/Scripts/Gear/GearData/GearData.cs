using UnityEngine;

namespace Gear_Data {

    [CreateAssetMenu(fileName = "GearData", menuName = "Resource/Gears/GearData")]
    public class GearData : ScriptableObject {
        [SerializeField] private GearRaretyData gearRaretyData;
        [SerializeField] private RankStatData rankStatData;
        [SerializeField] private RankStatData droneRankStatData;
        [SerializeField] private GearTypeData gearTypeData;

        public GearRaretyData GearRaretyData { get => gearRaretyData; }
        public RankStatData RankStatData { get => rankStatData; }
        public GearTypeData GearTypeData { get => gearTypeData; }
        public RankStatData DroneRankStatData { get => droneRankStatData; }
    }
}
