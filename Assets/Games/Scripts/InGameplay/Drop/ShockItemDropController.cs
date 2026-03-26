
using Helper;
using UnityEngine;

public class ShockItemDropController : BaseDropController {
    [SerializeField] private Item ShockItem;
    [SerializeField] private float moveDownSpeed;

    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        GameResources.Instance.Inventory.AddXCandy(1);
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnDropXmasCandy);
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
