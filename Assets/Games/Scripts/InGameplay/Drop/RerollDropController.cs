
using Helper;
using UnityEngine;

public class RerollDropController : BaseDropController {
    [SerializeField] private Item reroll;
    [SerializeField] private float moveDownSpeed;

    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        GameResources.Instance.Inventory.Add(reroll.Id, 1);
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
