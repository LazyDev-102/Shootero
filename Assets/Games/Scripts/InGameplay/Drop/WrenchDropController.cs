

using Helper;
using UnityEngine;

public class WrenchDropController : BaseDropController {
    [SerializeField] private float hp;
    [SerializeField] private float moveDownSpeed;

    public void SetHp(float value) {
        hp = value;
    }
    public override void AddToShip(ShipBase ship) {
        if (isApplied) {
            return;
        }
        isApplied = true;
        int hpGet = Mathf.CeilToInt(ship.ShipStat.MaxHP.Value * hp * 0.01f);
        ship.ShipHealth.AddHpWithHealingEffect(hpGet, hasHpUp: true);
        SoundManager.Instance.PlayHealTake();
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
