using Gemmob;
using UnityEngine;
namespace Gear_Data {

    [CreateAssetMenu(fileName = "StatHardData", menuName = "Resource/Gears/ItemStat/StatHardData")]
    public class StatHardData : ScriptableObject {
        [SerializeField] private int id;
        [SerializeField] private string description;
        [SerializeField] private EventKey.StatEvent statEvent;
        [SerializeField] private GetDescriptionStat descriptionFormat;

        public int Id { get => id; }
        public string Description { get => description; }
        public EventKey.StatEvent StatEvent { get => statEvent; }

        public string GetDescription(float value) {
            return descriptionFormat.GetDescriotion(description, value);
        }

        public string GetValueString(float value) {
            return descriptionFormat.GetValueString(value);
        }

        public void AddStat(StatModifier statValue) {
            EventDispatcher.Instance.Dispatch((int)statEvent, new StatValueParam() {
                value = statValue,
                isAdd = true
            });
        }

        public void RemoveStat(StatModifier statValue) {
            EventDispatcher.Instance.Dispatch((int)statEvent, new StatValueParam() {
                value = statValue,
                isAdd = false
            });
        }
    }

    public struct StatValueParam : IEventParams {
        public StatModifier value;
        public bool isAdd;
    }

    [System.Serializable]
    public class StatHardInfo {
        [SerializeField] private StatHardData statData;
        [SerializeField] private StatModifier statValue;

        public StatHardData StatData { get => statData; }
        public StatModifier StatValue { get => statValue; }
    }
}

