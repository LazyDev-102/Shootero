using UnityEngine;

[CreateAssetMenu(fileName = "ItemCollector", menuName = "Resource/Item/ItemDatabase/ItemCollector")]
public class ItemCollector : ScriptableObject {
    [SerializeField] private string nameCollector = "other";
    [SerializeField] private Item[] items;

    public Item[] Items { get => items; }
    public string NameCollector { get => nameCollector; }
}
