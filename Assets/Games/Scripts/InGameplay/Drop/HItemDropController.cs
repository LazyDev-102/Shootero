
using Helper;
using UnityEngine;

public class HItemDropController : BaseDropController {
    [SerializeField] private Item hItem;
    [SerializeField] private float moveDownSpeed;

    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        GameResources.Instance.Inventory.AddHCandy(1);
        Gemmob.EventDispatcher.Instance.Dispatch(EventKey.OnDropHalloweenCandy);
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
