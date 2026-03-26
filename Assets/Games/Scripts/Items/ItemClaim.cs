using GameSystem.Common.UnityInspector;
using Gear_Data;
using System;
using UnityEngine;

[Serializable]
public class ItemClaim {
    [SerializeField, ItemField] private int id;
    [SerializeField] private int amount;

    private IItem item;


    public int Id { get => id; set => id = value; }
    public int Amount { get => amount; set => amount = value; }
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
    public bool IsEmpty => Id == ItemDatabase.NoneId || amount <= 0;



    public ItemClaim(int id, int amount) {
        Id = id;
        Amount = amount;
    }


    public IItem Item {
        get {
            if (item == null) {
                ItemDatabase.TryGetItem(Id, out item);
            }
            return item;
        }
    }

    public virtual void Claim() {
        IItem item = Item;
        if (item != null) {
            item.Claim(Amount);
        }
    }

    public void Claim(float multi) {
        IItem item = Item;
        if (item != null) {
            item.Claim(Mathf.RoundToInt(Amount * multi));
        }
    }
}


public class RandomGearClaimItem : Item {
    [SerializeField] private int rankIndex;
    [SerializeField] private ItemCollector gearColletor;
    public override void Claim(int amount) {

    }
}

public class GearClaimItem : Item {
    [SerializeField] private int rankIndex;
    [SerializeField] private GearHardData gear;
    public override void Claim(int amount) {

    }
}

