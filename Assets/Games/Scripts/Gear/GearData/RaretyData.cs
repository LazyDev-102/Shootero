using UnityEngine;

[CreateAssetMenu(fileName = "GearRarety", menuName = "Resource/Gears/GearRatery")]
public class RaretyData : ScriptableObject {
    [SerializeField] private int index;
    [SerializeField] private string nameRarety;
    [SerializeField] private Color color;
    [SerializeField] private Sprite frame;
    [SerializeField] private string tagName;
    [SerializeField] private Rarety type;

    public int Index { get => index; }
    public string NameRarety { get => nameRarety; }
    public Color Color { get => color; }
    public Sprite Frame { get => frame; }
    public string TagName { get => tagName; }
    public Rarety Type { get => type; }
}
