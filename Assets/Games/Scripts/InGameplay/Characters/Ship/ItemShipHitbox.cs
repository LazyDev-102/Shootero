using UnityEngine;

public class ItemShipHitbox : MonoBehaviour {
    [SerializeField] private ShipBase shipBase;
    private void OnTriggerEnter2D(Collider2D collision) {
        BaseDropController dropController = collision.GetComponent<BaseDropController>();
        if (dropController) {
            dropController.AddToShip(shipBase);
        }
    }
}
