using UnityEngine;

[CreateAssetMenu(fileName = "SuffixFlatDescriptionStat", menuName = "Resource/Gears/ItemStat/DescriptionStat/SuffixFlat")]

public class SuffixFlatDescriptionStat : GetDescriptionStat {
    [SerializeField] private string special;
    public override string GetDescriotion(string description, float value) {
        return $"{description} + {value}{special}";
    }

    public override string GetValueString(float value) {
        return $"{value}{special}";
    }
}
