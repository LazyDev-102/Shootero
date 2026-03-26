using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "BuyShipRegister", menuName = "Resource/Notifications/Registers/Buy Ship Register")]
public class BuyShipRegister : NotiRegister<ShipInfor> {
    [System.Serializable] public class UnityEventItem : UnityEvent<ShipInfor> { }

    [SerializeField] private UnityEventItem onUpdate;
    public override UnityEvent<ShipInfor> OnUpdate { get => onUpdate; }
}