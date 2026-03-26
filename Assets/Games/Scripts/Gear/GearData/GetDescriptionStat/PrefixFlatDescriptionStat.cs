using UnityEngine;

[CreateAssetMenu(fileName = "PrefixFlatDescriptionStat", menuName = "Resource/Gears/ItemStat/DescriptionStat/PrefixFlat")]
public class PrefixFlatDescriptionStat : GetDescriptionStat {
    [SerializeField] private string special;
    public override string GetDescriotion(string description, float value) {
        return $"+ {value} {description}";
    }

    public override string GetValueString(float value) {
        return $"{value}{special}";
    }
}
