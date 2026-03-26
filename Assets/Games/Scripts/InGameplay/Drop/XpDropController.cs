
using Helper;
using UnityEngine;

public class XpDropController : BaseDropController {
    [SerializeField] private Item Xp;
    [SerializeField] private float moveDownSpeed;
    [SerializeField] private int xpValue;

    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        if (ship != null)
            ship.ShipLevel.AddExp(xpValue);
        Destroy();
    }
    public void SetValue(int value) {
        xpValue = value;
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
