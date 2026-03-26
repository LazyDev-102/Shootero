using UnityEngine;

[CreateAssetMenu(fileName = "SuffixPercentDescriptionStat", menuName = "Resource/Gears/ItemStat/DescriptionStat/SuffixPercent")]

public class SuffixPercentDescriptionStat : GetDescriptionStat {
    public override string GetDescriotion(string description, float value) {
        return $"{description} + {value * 100}%";
    }

    public override string GetValueString(float value) {
        return $"{value * 100}%";
    }
}
