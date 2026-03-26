using UnityEngine;

[CreateAssetMenu(fileName = "PrefixPercentDescriptionStat", menuName = "Resource/Gears/ItemStat/DescriptionStat/PrefixPercent")]

public class PrefixPercentDescriptionStat : GetDescriptionStat {
    public override string GetDescriotion(string description, float value) {
        return $"+ {value * 100}% {description}";
    }

    public override string GetValueString(float value) {
        return $"{value * 100}%";
    }
}
