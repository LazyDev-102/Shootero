using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "FullHealData", menuName = "Resource/HardData/Offer/FullHealData")]
public class FullHealData : ScriptableObject {
    [SerializeField] private float percentHpCondition;
    private bool canAppear;

    public void Excute(ShipBase ship) {
        if (ship == null)
            return;
        canAppear = false;
        ship.ShipHealth.AddFullHP();
    }
    public void Reset() {
        canAppear = true;
    }
    public bool CanAppear(ShipBase ship) {
        if (ship == null)
            return false;
        return canAppear && ship.ShipHealth.GetPercentHPRemain() <= percentHpCondition;
    }
}
