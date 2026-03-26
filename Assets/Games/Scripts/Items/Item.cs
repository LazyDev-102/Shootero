using UnityEngine;
using GameSystem.Common.UnityInspector;

[CreateAssetMenu(fileName = "NewItem", menuName = "Resource/Item/BasicItem", order = 0)]
public class Item : ScriptableObject, IItem {
    [Header("[Item]")]
    [SerializeField, SpriteField] private Sprite icon;
    [SerializeField, Range(1, 9999)] private int id = 1;
    [SerializeField] private string displayName;
    [SerializeField, TextArea(3, 5)] private string description;
    [SerializeField] private ItemStack price;

    public int Id {
        get => id;
        set => id = value;
    }
    public string Name {
        get => displayName;
        set => displayName = value;
    }
    public virtual Sprite Icon => icon;
    public string Description {
        get => description;
        set => description = value;
    }
    public ItemStack Price => price;

    public virtual void Claim(int amount) {
        GameResources.Instance.Inventory.Add(Id, amount);
    }
}

