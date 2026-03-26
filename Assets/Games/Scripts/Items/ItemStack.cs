using UnityEngine;
using GameSystem.Common.UnityInspector;
using UnityEngine.Serialization;

[System.Serializable]
public class ItemStack : IItemInstance {
    public static ItemStack Empty = new ItemStack(ItemDatabase.NoneId, 0);

    [FormerlySerializedAs("item")]
    [SerializeField, ItemField] protected int i;
    [FormerlySerializedAs("amount")]
    [SerializeField] protected int a;

    private IItem item;

    public IItem Item {
        get {
            if (item == null) {
                ItemDatabase.TryGetItem(Id, out item);
            }
            return item;
        }
    }

    public int Amount {
        set {
            a = value;
        }

        get {
            return a;
        }
    }

    public int Id => i;

    public string Name {
        get {
            return Item?.Name;
        }
    }

    public string Description {
        get {
            return Item?.Description;
        }
    }

    public Sprite Icon {
        get {
            return Item?.Icon;
        }
    }

    public bool IsEmpty => Id == ItemDatabase.NoneId || a <= 0;

    public ItemStack(int itemId, int amount) {
        this.i = itemId;
        this.a = amount;
    }

    public override string ToString() {
        return $"{Name} - {Amount}";
    }

    public string ToShortString() {
        return $"{Id}{Amount}";
    }

}
