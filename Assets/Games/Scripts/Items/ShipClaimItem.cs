using UnityEngine;

[CreateAssetMenu(fileName = "ShipClaimItem", menuName = "Resource/Item/Ship/ShipClaimItem")]
public class ShipClaimItem : Item {
    [SerializeField] private int idShip;

    public int IdShip { get => idShip; }

    public override void Claim(int amount) {
        GameResources.Instance.Ship.BuyShip(idShip, amount);
        // unlock ship with id idShip
    }
}
