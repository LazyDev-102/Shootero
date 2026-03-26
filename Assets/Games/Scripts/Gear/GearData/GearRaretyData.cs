using UnityEngine;

[CreateAssetMenu(fileName = "GearRaretyData", menuName = "Resource/Gears/GearRaretyData")]
public class GearRaretyData : ScriptableObject {
    [SerializeField] private RaretyData[] raretyData;

    public RaretyData[] RaretyData { get => raretyData; }
}
