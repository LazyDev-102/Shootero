using Helper;
using UnityEngine;

public class ItemDropController : BaseDropController {
    [SerializeField] protected int idItem;
    [SerializeField] protected int amount;
    [SerializeField] protected float moveDownSpeed;
    [SerializeField] protected SpriteRenderer sprite;

    private Item item;

    public virtual void SetItem(Item item, int amount) {
        this.item = item;
        sprite.sprite = item.Icon;
        this.amount = amount;
    }
    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        GameResources.Instance.Inventory.Add(item.Id, amount);
        GameManager.Instance.AddClaimedItem(item.Id, amount);
        Destroy();
    }

    protected override void Update() {
        base.Update();
        Vector2 newPosition = myTransform.position;
        newPosition += Vector2.down * moveDownSpeed * Time.deltaTime;
        myTransform.position = newPosition;
        if (BorderHelper.IsOutBound(newPosition)) {
            Destroy();
        }
    }
}