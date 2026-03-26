using UnityEngine;

[CreateAssetMenu(fileName = "DropData", menuName = "Resource/HardData/Drop/DropData")]
public class DropData : ScriptableObject {
    [SerializeField] private ConquerorDropSystem[] dropSystem;
    public ConquerorDropSystem[] DropSystem { get => dropSystem; }
}
