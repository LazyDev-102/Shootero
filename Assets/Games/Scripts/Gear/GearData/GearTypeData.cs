using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GearTypeData", menuName = "Resource/Gears/GearTypeData")]
public class GearTypeData : ScriptableObject {
    [SerializeField] private List<GearTypeInfor> data;

    public List<GearTypeInfor> Data { get => data; }


    [Serializable]
    public class GearTypeInfor {
        [SerializeField] private GearType type;
        [SerializeField] private Sprite icon;
        public GearType Type { get => type; }
        public Sprite Icon { get => icon; }
    }

    public Sprite GetGearType(GearType type) {
        foreach (var item in data) {
            if (type == item.Type) {
                return item.Icon;
            }
        }
        return null;
    }
}
