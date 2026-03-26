

using Gear_Data;
using Helper;
using UnityEngine;

public class GearDropController : BaseDropController {
    [SerializeField] protected float moveDownSpeed;
    [SerializeField] protected SpriteRenderer sprite;

    private GearHardData gearItemData;


    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        gearItemData.AddNewGear();
        GameManager.Instance.AddClaimedItem(gearItemData.Id, 1);
        Destroy();
    }

    public void SetItem(Item item) {
        if (item is GearHardData gear) {
            gearItemData = gear;
        }
        sprite.sprite = item.Icon;

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
